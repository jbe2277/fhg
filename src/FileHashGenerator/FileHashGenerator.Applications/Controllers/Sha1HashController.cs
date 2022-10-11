﻿using System.Security.Cryptography;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Applications.Controllers;

internal class Sha1HashController : HashController
{
    public Sha1HashController(IMessageService messageService, IShellService shellService) : base(messageService, shellService)
    {
    }

    protected override async Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress)
    {
        using var stream = new ProgressStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), cancellationToken, progress);
        using var sha = SHA1.Create();
        return await sha.ComputeHashAsync(stream).ConfigureAwait(false);
    }
}
