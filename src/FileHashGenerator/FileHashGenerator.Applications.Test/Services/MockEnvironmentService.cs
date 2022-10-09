using System.ComponentModel.Composition;
using Waf.FileHashGenerator.Applications.Services;

namespace Test.FileHashGenerator.Applications.Services;

[Export, Export(typeof(IEnvironmentService))]
public class MockEnvironmentService : IEnvironmentService
{
    public IReadOnlyList<string> DocumentFileNames { get; set; } = Array.Empty<string>();
}
