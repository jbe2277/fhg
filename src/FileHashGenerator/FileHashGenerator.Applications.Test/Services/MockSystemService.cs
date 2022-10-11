using Waf.FileHashGenerator.Applications.Services;

namespace Test.FileHashGenerator.Applications.Services;

public class MockSystemService : ISystemService
{
    public IReadOnlyList<string> DocumentFileNames { get; set; } = Array.Empty<string>();
}
