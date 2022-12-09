using Microsoft.Extensions.DependencyInjection;
using System.Waf.Applications;
using Waf.FileHashGenerator.Applications.Controllers;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Applications.ViewModels;

namespace Waf.FileHashGenerator.Applications;

public static class ApplicationsModule
{
    public static IServiceCollection AddApplications(this IServiceCollection services) => services
        .AddSingletonAndSelf<IModuleController, ModuleController>()
        .AddFactory<MD5HashController>()
        .AddFactory<Sha1HashController>()
        .AddFactory<Sha256HashController>()
        .AddFactory<Sha512HashController>()
        .AddSingleton<FileHashListViewModel>()
        .AddSingleton<ShellViewModel>();
}
