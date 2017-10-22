using System;

namespace Waf.FileHashGenerator.Applications.Services
{
    internal class HexadecimalFormatter : IHashFormatter
    {
        public bool IsCaseSensitive { get { return false; } }
        
        public string FormatHash(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
