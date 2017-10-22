using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Presentation.Services
{
    [Export(typeof(IEnvironmentService))]
    internal class EnvironmentService : IEnvironmentService
    {
        private readonly Lazy<IEnumerable<string>> documentFileNames = 
            new Lazy<IEnumerable<string>>(() => Environment.GetCommandLineArgs().Skip(1).ToArray());


        public IEnumerable<string> DocumentFileNames { get { return documentFileNames.Value; } }
    }
}
