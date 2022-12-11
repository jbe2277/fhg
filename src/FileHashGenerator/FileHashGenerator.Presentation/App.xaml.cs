using Microsoft.UI.Xaml;
using System.Waf.Applications;
using Microsoft.Extensions.DependencyInjection;
using Waf.FileHashGenerator.Applications;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Presentation;

public partial class App : Application
{
    private IEnumerable<IModuleController> moduleControllers = Array.Empty<IModuleController>();

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddApplications().AddPresentation().AddSingletonLazySupport();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        moduleControllers = serviceProvider.GetServices<IModuleController>();
        foreach (var x in moduleControllers) x.Initialize();
        foreach (var x in moduleControllers) x.Run();
    }
}
