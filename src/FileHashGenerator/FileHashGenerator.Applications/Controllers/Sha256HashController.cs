using System.ComponentModel.Composition;
using System.Security.Cryptography;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Applications.Controllers;

[Export, PartCreationPolicy(CreationPolicy.NonShared)]
internal class Sha256HashController : HashController
{
    [ImportingConstructor]
    public Sha256HashController(IMessageService messageService, IShellService shellService) : base(messageService, shellService)
    {
    }

    protected override Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress)
    {
        return Task.Run(() =>
        {
            using var stream = new ProgressStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), cancellationToken, progress);
            using var sha = SHA256.Create();
            return sha.ComputeHash(stream);
        });
    }
}
