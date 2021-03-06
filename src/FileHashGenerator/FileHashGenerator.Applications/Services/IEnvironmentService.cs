﻿using System.Collections.Generic;

namespace Waf.FileHashGenerator.Applications.Services
{
    public interface IEnvironmentService
    {
        IReadOnlyList<string> DocumentFileNames { get; }
    }
}
