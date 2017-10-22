﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Waf.Applications;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.Controllers
{
    [Export(typeof(IModuleController)), Export]
    internal class ModuleController : IModuleController
    {
        private readonly IMessageService messageService;
        private readonly IFileDialogService fileDialogService;
        private readonly IEnvironmentService environmentService;
        private readonly HexadecimalFormatter hexadecimalFormatter;
        private readonly Base64Formatter base64Formatter;
        private readonly ExportFactory<Sha512HashController> sha512HashControllerFactory;
        private readonly ExportFactory<Sha256HashController> sha256HashControllerFactory;
        private readonly ExportFactory<Sha1HashController> sha1HashControllerFactory;
        private readonly ExportFactory<MD5HashController> md5HashControllerFactory;
        private readonly Lazy<ShellViewModel> shellViewModel;
        private readonly Lazy<FileHashListViewModel> fileHashListViewModel;
        private readonly ExportFactory<AboutViewModel> aboutViewModelFactory;
        private readonly DelegateCommand openCommand;
        private readonly DelegateCommand closeCommand;
        private readonly DelegateCommand aboutCommand;
        private readonly FileHashRoot root;
        private IHashFormatter hashFormatter;
        private HashController hashController;


        [ImportingConstructor]
        public ModuleController(IMessageService messageService, IFileDialogService fileDialogService, IEnvironmentService environmentService,
            ExportFactory<Sha512HashController> sha512HashControllerFactory, ExportFactory<Sha256HashController> sha256HashControllerFactory,
            ExportFactory<Sha1HashController> sha1HashControllerFactory, ExportFactory<MD5HashController> md5HashControllerFactory,
            Lazy<ShellViewModel> shellViewModel, Lazy<FileHashListViewModel> fileHashListViewModel, ExportFactory<AboutViewModel> aboutViewModelFactory)
        {
            this.messageService = messageService;
            this.fileDialogService = fileDialogService;
            this.environmentService = environmentService;
            this.hexadecimalFormatter = new HexadecimalFormatter();
            this.base64Formatter = new Base64Formatter();
            this.sha512HashControllerFactory = sha512HashControllerFactory;
            this.sha256HashControllerFactory = sha256HashControllerFactory;
            this.sha1HashControllerFactory = sha1HashControllerFactory;
            this.md5HashControllerFactory = md5HashControllerFactory;
            this.shellViewModel = shellViewModel;
            this.fileHashListViewModel = fileHashListViewModel;
            this.aboutViewModelFactory = aboutViewModelFactory;

            this.openCommand = new DelegateCommand(OpenFile);
            this.closeCommand = new DelegateCommand(CloseFile);
            this.aboutCommand = new DelegateCommand(ShowAboutView);

            this.root = new FileHashRoot();
        }


        private ShellViewModel ShellViewModel { get { return shellViewModel.Value; } }

        private FileHashListViewModel FileHashListViewModel { get { return fileHashListViewModel.Value; } }

        private IHashFormatter HashFormatter
        {
            get { return hashFormatter; }
            set
            {
                if (hashFormatter != value)
                {
                    hashFormatter = value;
                    if (hashController != null)
                    {
                        hashController.HashFormatter = value;
                    }
                }
            }
        }


        public void Initialize()
        {
            FileHashListViewModel.FileHashItems = root.FileHashItems;
            FileHashListViewModel.OpenFilesAction = OpenCore;
            FileHashListViewModel.CloseCommand = closeCommand;
            ShellViewModel.OpenCommand = openCommand;
            ShellViewModel.AboutCommand = aboutCommand;
            ShellViewModel.ContentView = FileHashListViewModel.View;
            ShellViewModel.HashMode = HashMode.Sha1;
            ShellViewModel.PropertyChanged += ShellViewModelPropertyChanged;
            UpdateHashMode();
            UpdateFormatter();
        }

        public void Run()
        {
            ShellViewModel.Show();
            if (environmentService.DocumentFileNames.Any())
            {
                OpenCore(environmentService.DocumentFileNames);
            }
        }

        public void Shutdown()
        {
            try
            {
                Settings.Default.Save();
            }
            catch (Exception)
            {
                // When more application instances are closed at the same time then an exception occurs.
            }
        }

        private void OpenFile()
        {
            FileDialogResult result = fileDialogService.ShowOpenFileDialog(ShellViewModel.View, new FileType(Resources.AllFiles, ".*"));
            if (result.IsValid)
            {
                OpenCore(new[] { result.FileName });
            }
        }

        private void CloseFile(object parameter)
        {
            var item = (FileHashItem)parameter;
            root.RemoveFileHashItem(item);
        }

        private void OpenCore(IEnumerable<string> fileNames)
        {
            List<string> filesNotFound = new List<string>();

            foreach (string fileName in fileNames)
            {
                if (!root.FileHashItems.Any(x => x.FileName == fileName))
                {
                    if (File.Exists(fileName))
                    {
                        root.AddNewFileHashItem(fileName);
                    }
                    else
                    {
                        filesNotFound.Add(fileName);
                    }
                }
            }

            if (filesNotFound.Any())
            {
                messageService.ShowError(ShellViewModel.View, string.Format(CultureInfo.CurrentCulture, Resources.FilesNotFoundError, 
                    string.Join(Environment.NewLine, filesNotFound)));
            }
        }

        private void UpdateHashMode()
        {
            if (ShellViewModel.HashMode == HashMode.Sha512)
            {
                UpdateHashModeCore(sha512HashControllerFactory.CreateExport().Value, Resources.Sha512);
            }
            else if (ShellViewModel.HashMode == HashMode.Sha256)
            {
                UpdateHashModeCore(sha256HashControllerFactory.CreateExport().Value, Resources.Sha256);
            }
            else if (ShellViewModel.HashMode == HashMode.Sha1)
            {
                UpdateHashModeCore(sha1HashControllerFactory.CreateExport().Value, Resources.Sha1);
            }
            else
            {
                UpdateHashModeCore(md5HashControllerFactory.CreateExport().Value, Resources.MD5);
            }
        }

        private void UpdateHashModeCore(HashController newHashController, string header)
        {
            CancelActiveController();

            hashController = newHashController;
            hashController.Root = root;
            hashController.HashFormatter = HashFormatter;
            hashController.Initialize();

            FileHashListViewModel.HashHeader = header;
        }

        private void CancelActiveController()
        {
            if (hashController != null) { hashController.Shutdown(); }
            hashController = null;
        }

        private void ShellViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HashMode")
            {
                UpdateHashMode();
            }
            else if (e.PropertyName == "IsHexadecimalFormatting" || e.PropertyName == "IsBase64Formatting")
            {
                UpdateFormatter();
            }
        }

        private void UpdateFormatter()
        {
            if (ShellViewModel.IsHexadecimalFormatting)
            {
                HashFormatter = hexadecimalFormatter;
            }
            else
            {
                HashFormatter = base64Formatter;
            }
        }

        private void ShowAboutView()
        {
            var aboutViewModel = aboutViewModelFactory.CreateExport().Value;
            aboutViewModel.ShowDialog(ShellViewModel.View);
        }
    }
}