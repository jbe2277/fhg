namespace Waf.FileHashGenerator.Applications.Services;

internal class HexadecimalFormatter : IHashFormatter
{
    public bool IsCaseSensitive => false;
    
    public string FormatHash(byte[] hash) => BitConverter.ToString(hash).Replace("-", "", StringComparison.Ordinal);
}
