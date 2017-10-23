using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.Controllers
{
    internal abstract class HashController
    {
        private readonly IMessageService messageService;
        private readonly IShellService shellService;
        private readonly Dictionary<FileHashItem, CancellationTokenSource> cancellationTokenSources;
        private IHashFormatter hashFormatter;


        public HashController(IMessageService messageService, IShellService shellService)
        {
            this.messageService = messageService;
            this.shellService = shellService;
            cancellationTokenSources = new Dictionary<FileHashItem, CancellationTokenSource>();
        }


        public FileHashRoot Root { get; set; }

        internal IHashFormatter HashFormatter 
        {
            get => hashFormatter;
            set 
            { 
                if (hashFormatter != value)
                {
                    hashFormatter = value;
                    if (Root != null)
                    {
                        foreach (var item in Root.FileHashItems)
                        {
                            item.IsCaseSensitive = value.IsCaseSensitive;
                            item.Hash = value.FormatHash(item.HashBytes);
                        }
                    }
                }
            }
        }


        public void Initialize()
        {
            foreach (var item in Root.FileHashItems)
            {
                ComputeHash(item);
            }
            CollectionChangedEventManager.AddHandler((INotifyCollectionChanged)Root.FileHashItems, FileHashItemsCollectionChanged);
        }

        public void Shutdown()
        {
            CollectionChangedEventManager.RemoveHandler((INotifyCollectionChanged)Root.FileHashItems, FileHashItemsCollectionChanged);
            foreach (var cts in cancellationTokenSources.Values) { cts.Cancel(); }
            cancellationTokenSources.Clear();
        }

        protected abstract Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress);

        private void FileHashItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (FileHashItem item in e.NewItems)
                {
                    ComputeHash(item);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (FileHashItem item in e.OldItems)
                {
                    cancellationTokenSources[item].Cancel();
                    cancellationTokenSources.Remove(item);
                    shellService.RemoveProgress(item);
                }
            }
        }

        private async void ComputeHash(FileHashItem item)
        {
            var cts = new CancellationTokenSource();
            cancellationTokenSources.Add(item, cts);
            var progress = new Progress<double>(p => ProgressChanged(item, p));

            item.IsCaseSensitive = HashFormatter.IsCaseSensitive;
            item.Hash = null;
            item.Progress = 0;
            shellService.UpdateProgress(item, 0);

            try
            {
                item.HashBytes = await ComputeHashCoreAsync(item.FileName, cts.Token, progress);
                item.Hash = HashFormatter.FormatHash(item.HashBytes);
            }
            catch (OperationCanceledException)
            {
                // Ignore the canceled exception
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                messageService.ShowError(shellService.ShellView, string.Format(CultureInfo.CurrentCulture, Resources.UnknownComputeHashError, e));
            }
            finally
            {
                shellService.RemoveProgress(item);
            }
        }

        private void ProgressChanged(FileHashItem item, double progress)
        {
            item.Progress = progress;
            shellService.UpdateProgress(item, progress);
        }
    }
}
