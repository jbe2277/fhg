using System.Diagnostics;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.Controllers;

internal abstract class HashController(IMessageService messageService, IShellService shellService)
{
    private readonly Dictionary<FileHashItem, CancellationTokenSource> cancellationTokenSources = [];
    private IHashFormatter hashFormatter = null!;
    private IWeakEventProxy? fileHashItemsCollectionChangedProxy;

    public FileHashRoot Root { get; set; } = null!;

    internal IHashFormatter HashFormatter 
    {
        get => hashFormatter;
        set 
        {
            if (hashFormatter == value) return;
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

    public void Initialize()
    {
        foreach (var item in Root.FileHashItems) ComputeHash(item);
        fileHashItemsCollectionChangedProxy = WeakEvent.CollectionChanged.Add(Root.FileHashItems, FileHashItemsCollectionChanged);
    }

    public void Shutdown()
    {
        fileHashItemsCollectionChangedProxy?.Remove();
        foreach (var cts in cancellationTokenSources.Values) cts.Cancel();
        cancellationTokenSources.Clear();
    }

    protected abstract Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress);

    private void FileHashItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (FileHashItem item in e.NewItems ?? Array.Empty<FileHashItem>()) ComputeHash(item);
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (FileHashItem item in e.OldItems ?? Array.Empty<FileHashItem>())
            {
                cancellationTokenSources[item].Cancel();
                cancellationTokenSources.Remove(item);
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
            messageService.ShowError(shellService.ShellView, Resources.UnknownComputeHashError, e);
        }
    }

    private static void ProgressChanged(FileHashItem item, double progress) => item.Progress = progress;
}
