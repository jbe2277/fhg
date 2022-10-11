using System.ComponentModel.Composition;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Presentation.Services;

[Export(typeof(ISystemService))]
internal class SystemService : ISystemService
{
    public IReadOnlyList<string> DocumentFileNames { get; } = Environment.GetCommandLineArgs().Skip(1).ToArray();
}
