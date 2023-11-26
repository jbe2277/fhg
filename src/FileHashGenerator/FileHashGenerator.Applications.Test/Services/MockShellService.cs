using Test.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Applications.Services;

namespace Test.FileHashGenerator.Applications.Services;

public class MockShellService(MockShellView shellView) : IShellService
{
    public object ShellView { get; } = shellView;

    public Dictionary<object, double> ProgressReports { get; } = [];

    public void UpdateProgress(object source, double progress) => ProgressReports[source] = progress;

    public void RemoveProgress(object source) => ProgressReports.Remove(source);
}
