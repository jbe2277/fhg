using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using Waf.FileHashGenerator.Applications;
using Waf.FileHashGenerator.Applications.Services;

namespace Waf.FileHashGenerator.Presentation;

public partial class App : Application
{
    private IEnumerable<IModuleController> moduleControllers = Array.Empty<IModuleController>();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

#if (!DEBUG)
        DispatcherUnhandledException += AppDispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
#endif
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddApplications().AddPresentation().AddSingletonLazySupport();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

        moduleControllers = serviceProvider.GetServices<IModuleController>();
        foreach (var x in moduleControllers) x.Initialize();
        foreach (var x in moduleControllers) x.Run();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        foreach (var x in moduleControllers.Reverse()) x.Shutdown();
        base.OnExit(e);
    }

    private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) => HandleException(e.Exception, false);

    private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e) => HandleException(e.ExceptionObject as Exception, e.IsTerminating);

    private static void HandleException(Exception? e, bool isTerminating)
    {
        if (e is null) return;
        Trace.TraceError(e.ToString());
        if (!isTerminating)
        {
            MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Presentation.Properties.Resources.UnknownError, e.ToString()), ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
