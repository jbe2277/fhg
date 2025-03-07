namespace Waf.FileHashGenerator.Applications.Services;

internal class HexadecimalFormatter : IHashFormatter
{
    public bool IsCaseSensitive => false;
    
    public string FormatHash(byte[] hash) => Convert.ToHexString(hash);
}
