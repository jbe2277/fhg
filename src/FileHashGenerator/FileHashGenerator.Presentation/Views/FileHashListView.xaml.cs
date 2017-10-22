using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Controls;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Presentation.Views
{
    [Export(typeof(IFileHashListView))]
    public partial class FileHashListView : UserControl, IFileHashListView
    {
        private readonly Lazy<FileHashListViewModel> viewModel;

        
        public FileHashListView()
        {
            InitializeComponent();
            this.viewModel = new Lazy<FileHashListViewModel>(() => ViewHelper.GetViewModel<FileHashListViewModel>(this));
        }


        private FileHashListViewModel ViewModel { get { return viewModel.Value; } }


        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ViewModel.OpenFilesAction(files);
            }
        }
    }
}
