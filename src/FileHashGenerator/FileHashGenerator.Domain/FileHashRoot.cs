namespace Waf.FileHashGenerator.Domain;

public class FileHashRoot : Model
{
    private readonly ObservableCollection<FileHashItem> fileHashItems = new();

    public IReadOnlyList<FileHashItem> FileHashItems => fileHashItems;

    public FileHashItem AddNewFileHashItem(string fileName)
    {
        var fileHashItem = new FileHashItem(fileName);
        fileHashItems.Add(fileHashItem);
        return fileHashItem;
    }

    public void RemoveFileHashItem(FileHashItem fileHashItem) => fileHashItems.Remove(fileHashItem);
}
