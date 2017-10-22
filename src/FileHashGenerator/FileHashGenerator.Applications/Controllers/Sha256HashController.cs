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
    internal class Sha256HashController : HashController
    {
        [ImportingConstructor]
        public Sha256HashController(IMessageService messageService, IShellService shellService)
            : base(messageService, shellService)
        {
        }


        protected override Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress)
        {
            return Task<byte[]>.Factory.StartNew(() =>
            {
                using (var stream = new ProgressStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read),
                    cancellationToken, progress))
                {
                    using (var sha = new SHA256Cng())
                    {
                        return sha.ComputeHash(stream);
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
