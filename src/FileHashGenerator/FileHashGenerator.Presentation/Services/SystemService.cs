using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Presentation.Services;

public class SystemService : ISystemService
{
    public IReadOnlyList<string> DocumentFileNames { get; } = Environment.GetCommandLineArgs().Skip(1).ToArray();
}
