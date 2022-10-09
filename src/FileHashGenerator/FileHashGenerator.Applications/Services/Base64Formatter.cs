namespace Waf.FileHashGenerator.Applications.Services;

internal class Base64Formatter : IHashFormatter
{
    public bool IsCaseSensitive => true;
    
    public string FormatHash(byte[] hash) => Convert.ToBase64String(hash);
}
