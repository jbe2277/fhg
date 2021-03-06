﻿using System;
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
    internal class Sha1HashController : HashController
    {
        [ImportingConstructor]
        public Sha1HashController(IMessageService messageService, IShellService shellService) : base(messageService, shellService)
        {
        }

        protected override Task<byte[]> ComputeHashCoreAsync(string fileName, CancellationToken cancellationToken, IProgress<double> progress)
        {
            return Task.Run(() =>
            {
                using var stream = new ProgressStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), cancellationToken, progress);
                using var sha = SHA1.Create();
                return sha.ComputeHash(stream);
            });
        }
    }
}
