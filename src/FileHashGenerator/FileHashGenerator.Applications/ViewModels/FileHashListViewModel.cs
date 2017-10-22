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
        private IEnumerable<FileHashItem> fileHashItems;
        private string hashHeader;
        private ICommand closeCommand;

        
        [ImportingConstructor]
        public FileHashListViewModel(IFileHashListView view) : base(view)
        {
        }


        public IEnumerable<FileHashItem> FileHashItems
        {
            get { return fileHashItems; }
            set { SetProperty(ref fileHashItems, value); }
        }

        public string HashHeader
        {
            get { return hashHeader; }
            set { SetProperty(ref hashHeader, value); }
        }

        public Action<IEnumerable<string>> OpenFilesAction { get; set; }

        public ICommand CloseCommand
        {
            get { return closeCommand; }
            set { SetProperty(ref closeCommand, value); }
        }
    }
}
