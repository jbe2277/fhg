using Microsoft.Extensions.DependencyInjection;
using System.Waf.Applications.Services;
using System.Waf.UnitTesting.Mocks;
using Test.FileHashGenerator.Applications.Services;
using Test.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Applications.Views;

namespace Test.FileHashGenerator.Applications;

public static class ApplicationsTestModule
{
    public static IServiceCollection AddApplicationsTest(this IServiceCollection services) => services
        .AddSingletonAndSelf<IMessageService, MockMessageService>()
        .AddSingletonAndSelf<IFileDialogService, MockFileDialogService>()
        .AddSingletonAndSelf<ISettingsService, MockSettingsService>()

        .AddSingletonAndSelf<IShellService, MockShellService>()
        .AddSingletonAndSelf<ISystemService, MockSystemService>()
        .AddTransient<IAboutView, MockAboutView>()
        .AddSingletonAndSelf<IFileHashListView, MockFileHashListView>()
        .AddSingletonAndSelf<IShellView, MockShellView>();
}
