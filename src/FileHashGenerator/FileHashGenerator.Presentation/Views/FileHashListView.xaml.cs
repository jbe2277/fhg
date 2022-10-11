using System.Waf.Applications;
using System.Windows;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Presentation.Views;

public partial class FileHashListView : IFileHashListView
{
    private readonly Lazy<FileHashListViewModel> viewModel;

    public FileHashListView()
    {
        InitializeComponent();
        viewModel = new Lazy<FileHashListViewModel>(this.GetViewModel<FileHashListViewModel>()!);
    }

    private FileHashListViewModel ViewModel => viewModel.Value;

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
