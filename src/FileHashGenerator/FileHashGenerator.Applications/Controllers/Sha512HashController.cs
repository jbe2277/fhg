﻿using System.ComponentModel.Composition;
using System.Security.Cryptography;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Applications.Controllers;

[Export, PartCreationPolicy(CreationPolicy.NonShared)]
internal class Sha512HashController : HashController
{
    [ImportingConstructor]
    public Sha512HashController(IMessageService messageService, IShellService shellService) : base(messageService, shellService)
    {
    }

    protected override async Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress)
    {
        using var stream = new ProgressStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), cancellationToken, progress);
        using var sha = SHA512.Create();
        return await sha.ComputeHashAsync(stream).ConfigureAwait(false);
    }
}
