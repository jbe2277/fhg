﻿using System.Security.Cryptography;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Applications.Controllers;

internal class MD5HashController(IMessageService messageService, IShellService shellService) : HashController(messageService, shellService)
{
    protected override async Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress)
    {
        using var stream = new ProgressStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), cancellationToken, progress);
        using var md5 = MD5.Create();
        return await md5.ComputeHashAsync(stream).ConfigureAwait(false);
    }
}
