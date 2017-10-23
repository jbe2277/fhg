using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Applications.Controllers
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    internal class MD5HashController : HashController
    {
        [ImportingConstructor]
        public MD5HashController(IMessageService messageService, IShellService shellService)
            : base(messageService, shellService)
        {
        }


        protected override Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress)
        {
            return Task.Run(() =>
            {
                using (var stream = new ProgressStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read),
                    cancellationToken, progress))
                {
                    using (var md5 = new MD5Cng())
                    {
                        return md5.ComputeHash(stream);
                    }
                }
            });
        }
    }
}
