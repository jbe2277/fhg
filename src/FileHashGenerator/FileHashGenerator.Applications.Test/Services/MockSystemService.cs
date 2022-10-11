using System.ComponentModel.Composition;
using Waf.FileHashGenerator.Applications.Services;

namespace Test.FileHashGenerator.Applications.Services;

[Export, Export(typeof(ISystemService))]
public class MockSystemService : ISystemService
{
    public IReadOnlyList<string> DocumentFileNames { get; set; } = Array.Empty<string>();
}
