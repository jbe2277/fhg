using System.Waf.Applications;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.ViewModels;

public class FileHashListViewModel(IFileHashListView view) : ViewModel<IFileHashListView>(view)
{
    private string hashHeader = "";

    public IReadOnlyObservableList<FileHashItemModel> FileHashItems { get; private set; } = null!;

    public string HashHeader { get => hashHeader; set => SetProperty(ref hashHeader, value); }

    public Action<IReadOnlyList<string>> OpenFilesAction { get; set; } = null!;

    public ICommand CloseCommand { get; set; } = DelegateCommand.DisabledCommand;

    public void SetFileHashItems(IReadOnlyList<FileHashItem> list)
    {
        FileHashItems = new SynchronizingList<FileHashItemModel, FileHashItem>(list, x => new FileHashItemModel(this, x));
    }
}
