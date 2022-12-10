using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Presentation.Views;

namespace Waf.FileHashGenerator.Presentation.Services;

public class ShellService : IShellService
{
    private readonly Lazy<ShellWindow> shellView;

    public ShellService(Lazy<ShellWindow> shellView)
    {
        this.shellView = shellView;
    }
    
    public object ShellView => shellView.Value;
}
