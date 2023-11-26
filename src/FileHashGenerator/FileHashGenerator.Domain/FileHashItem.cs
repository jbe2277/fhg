namespace Waf.FileHashGenerator.Domain;

public class FileHashItem(string fileName) : Model
{
    private byte[] hashBytes = [];
    private string? hash;
    private string? expectedHash;
    private bool isCaseSensitive;
    private double progress;
    private bool? isHashValid;

    public string FileName { get; } = fileName;

    public byte[] HashBytes { get => hashBytes; set => SetProperty(ref hashBytes, value); }

    public string? Hash
    {
        get => hash;
        set
        {
            if (!SetProperty(ref hash, value)) return;
            UpdateIsHashValid();
        }
    }

    public string? ExpectedHash
    {
        get => expectedHash;
        set
        {
            if (!SetProperty(ref expectedHash, value)) return;
            UpdateIsHashValid();
        }
    }

    public bool IsCaseSensitive 
    { 
        get => isCaseSensitive;
        set 
        {
            if (!SetProperty(ref isCaseSensitive, value)) return;
            UpdateIsHashValid();
        }
    }

    public double Progress { get => progress; set => SetProperty(ref progress, value); }

    public bool? IsHashValid { get => isHashValid; private set => SetProperty(ref isHashValid, value); }

    private void UpdateIsHashValid()
    {
        IsHashValid = string.IsNullOrEmpty(Hash) || string.IsNullOrEmpty(ExpectedHash) 
            ? null 
            : string.Equals(Hash, ExpectedHash, IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
    }
}
