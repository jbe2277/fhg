using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Waf.FileHashGenerator.Applications.Services
{
    public interface IEnvironmentService
    {
        IEnumerable<string> DocumentFileNames { get; }
    }
}
