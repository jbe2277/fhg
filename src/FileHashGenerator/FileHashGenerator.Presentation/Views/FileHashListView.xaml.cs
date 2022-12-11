using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Applications.Views;
using Windows.ApplicationModel.DataTransfer;

namespace Waf.FileHashGenerator.Presentation.Views;

public sealed partial class FileHashListView : UserControl, IFileHashListView
{
    public FileHashListView()
    {
        InitializeComponent();
    }

    public FileHashListViewModel ViewModel => (FileHashListViewModel)DataContext!;

    private void GridDragOver(object sender, DragEventArgs e) => e.AcceptedOperation = DataPackageOperation.Link;

    private async void GridDrop(object sender, DragEventArgs e)
    {
        if (e.DataView.Contains(StandardDataFormats.StorageItems))
        {
            var items = await e.DataView.GetStorageItemsAsync();
            ViewModel.OpenFilesAction(items.Select(x => x.Path).ToArray());
        }
    }
}
