﻿using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Waf;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Threading;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Applications.ViewModels;

namespace Waf.FileHashGenerator.Presentation;

public partial class App : Application
{
    private AggregateCatalog? catalog;
    private CompositionContainer? container;
    private IEnumerable<IModuleController> moduleControllers = Array.Empty<IModuleController>();

    public App()
    {
        var profileRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationInfo.ProductName, "ProfileOptimization");
        Directory.CreateDirectory(profileRoot);
        ProfileOptimization.SetProfileRoot(profileRoot);
        ProfileOptimization.StartProfile("Startup.profile");
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        DispatcherUnhandledException += AppDispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;

        catalog = new AggregateCatalog();
        catalog.Catalogs.Add(new AssemblyCatalog(typeof(WafConfiguration).Assembly));
        catalog.Catalogs.Add(new AssemblyCatalog(typeof(ShellViewModel).Assembly));
        catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

        container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
        var batch = new CompositionBatch();
        batch.AddExportedValue(container);
        container.Compose(batch);

        // Initialize all presentation services
        var presentationServices = container.GetExportedValues<IPresentationService>();
        foreach (var presentationService in presentationServices) { presentationService.Initialize(); }
        
        // Initialize and run all module controllers
        moduleControllers = container.GetExportedValues<IModuleController>();
        foreach (var moduleController in moduleControllers) { moduleController.Initialize(); }
        foreach (var moduleController in moduleControllers) { moduleController.Run(); }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        foreach (var moduleController in moduleControllers.Reverse()) { moduleController.Shutdown(); }
        container?.Dispose();
        catalog?.Dispose();
        base.OnExit(e);
    }

    private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        HandleException(e.Exception, false);
    }

    private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        HandleException(e.ExceptionObject as Exception, e.IsTerminating);
    }

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
