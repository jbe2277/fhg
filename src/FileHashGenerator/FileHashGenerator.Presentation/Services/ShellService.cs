using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Presentation.Views;

namespace Waf.FileHashGenerator.Presentation.Services;

public class ShellService(Lazy<ShellWindow> shellView) : IShellService
{
    private readonly Lazy<ShellWindow> shellView = shellView;

    public object ShellView => shellView.Value;
}
