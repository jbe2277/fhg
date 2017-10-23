namespace Waf.FileHashGenerator.Applications.Services
{
    public interface IHashFormatter
    {
        bool IsCaseSensitive { get; }
        
        string FormatHash(byte[] hash);
    }
}
