using System.ComponentModel.Composition;
using Test.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Applications.Services;

namespace Test.FileHashGenerator.Applications.Services;

[Export, Export(typeof(IShellService))]
public class MockShellService : IShellService
{
    [ImportingConstructor]
    public MockShellService(MockShellView shellView)
    {
        ShellView = shellView;
        ProgressReports = new Dictionary<object, double>();
    }
    
    public object ShellView { get; }

    public Dictionary<object, double> ProgressReports { get; }

    public void UpdateProgress(object source, double progress) => ProgressReports[source] = progress;

    public void RemoveProgress(object source) => ProgressReports.Remove(source);
}
