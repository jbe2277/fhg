using System.ComponentModel.Composition;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Presentation.Services;

[Export(typeof(IEnvironmentService))]
internal class EnvironmentService : IEnvironmentService
{
    public IReadOnlyList<string> DocumentFileNames { get; } = Environment.GetCommandLineArgs().Skip(1).ToArray();
}
