namespace Waf.FileHashGenerator.Domain;

public class FileHashItem(string fileName) : Model
{
    public string FileName { get; } = fileName;

    public byte[] HashBytes { get; set => SetProperty(ref field, value); } = [];

    public string? Hash
    {
        get;
        set
        {
            if (!SetProperty(ref field, value)) return;
            UpdateIsHashValid();
        }
    }

    public string? ExpectedHash
    {
        get;
        set
        {
            if (!SetProperty(ref field, value)) return;
            UpdateIsHashValid();
        }
    }

    public bool IsCaseSensitive 
    { 
        get;
        set 
        {
            if (!SetProperty(ref field, value)) return;
            UpdateIsHashValid();
        }
    }

    public double Progress { get; set => SetProperty(ref field, value); }

    public bool? IsHashValid { get; private set => SetProperty(ref field, value); }

    private void UpdateIsHashValid()
    {
        IsHashValid = string.IsNullOrEmpty(Hash) || string.IsNullOrEmpty(ExpectedHash) 
            ? null 
            : string.Equals(Hash, ExpectedHash, IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
    }
}
