using System.Waf.Applications;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.ViewModels;

public class FileHashListViewModel : ViewModel<IFileHashListView>
{
    private string hashHeader = "";
    private ICommand closeCommand = DelegateCommand.DisabledCommand;

    public FileHashListViewModel(IFileHashListView view) : base(view)
    {
    }

    public ReadOnlyObservableList<FileHashItemModel> FileHashItems { get; private set; } = null!;

    public string HashHeader { get => hashHeader; set => SetProperty(ref hashHeader, value); }

    public Action<IReadOnlyList<string>> OpenFilesAction { get; set; } = null!;

    public ICommand CloseCommand { get => closeCommand; set => SetProperty(ref closeCommand, value); }

    public void SetFileHashItems(IReadOnlyList<FileHashItem> list)
    {
        FileHashItems = new SynchronizingCollectionCore<FileHashItemModel, FileHashItem>(list, x => new FileHashItemModel(this, x));
    }
}
