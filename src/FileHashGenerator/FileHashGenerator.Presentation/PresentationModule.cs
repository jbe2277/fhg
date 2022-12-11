using Microsoft.Extensions.DependencyInjection;
using System.Waf.Applications.Services;
using System.Waf.Presentation.Services;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Presentation.Services;
using Waf.FileHashGenerator.Presentation.Views;

namespace Waf.FileHashGenerator.Presentation;

public static class PresentationModule
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) => services
        // TODO: These are WPF and not WinUI services - but they work
        .AddSingleton<IMessageService, MessageService>()
        .AddSingleton<IFileDialogService, FileDialogService>()
        .AddSingleton<ISettingsService, SettingsService>()

        .AddSingleton<IShellService, ShellService>()
        .AddSingleton<ISystemService, SystemService>()
        .AddSingleton<IFileHashListView, FileHashListView>()
        .AddSingletonAndSelf<IShellView, ShellWindow>();
}
