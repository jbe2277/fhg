using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.ViewModels
{
    [Export]
    public class FileHashListViewModel : ViewModel<IFileHashListView>
    {
        private IReadOnlyList<FileHashItem> fileHashItems;
        private string hashHeader;
        private ICommand closeCommand;

        
        [ImportingConstructor]
        public FileHashListViewModel(IFileHashListView view) : base(view)
        {
        }


        public IReadOnlyList<FileHashItem> FileHashItems
        {
            get => fileHashItems;
            set => SetProperty(ref fileHashItems, value);
        }

        public string HashHeader
        {
            get => hashHeader;
            set => SetProperty(ref hashHeader, value);
        }

        public Action<IReadOnlyList<string>> OpenFilesAction { get; set; }

        public ICommand CloseCommand
        {
            get => closeCommand;
            set => SetProperty(ref closeCommand, value);
        }
    }
}
