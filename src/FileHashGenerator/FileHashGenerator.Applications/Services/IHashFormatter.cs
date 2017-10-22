using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waf.FileHashGenerator.Applications.Services
{
    public interface IHashFormatter
    {
        bool IsCaseSensitive { get; }
        
        string FormatHash(byte[] hash);
    }
}
