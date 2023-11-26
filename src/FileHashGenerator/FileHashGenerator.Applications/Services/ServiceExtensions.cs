using Microsoft.Extensions.DependencyInjection;

namespace Waf.FileHashGenerator.Applications.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddSingletonAndSelf<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        return services.AddSingleton<TImplementation>().AddSingleton<TService, TImplementation>(x => x.GetRequiredService<TImplementation>());
    }

    public static IServiceCollection AddFactory<TService>(this IServiceCollection services) where TService : class
    {
        return services.AddTransient<TService>().AddSingleton<Func<TService>>(x => () => x.GetRequiredService<TService>());
    }

    public static IServiceCollection AddSingletonLazySupport(this IServiceCollection services) => services.AddSingleton(typeof(Lazy<>), typeof(ServiceLazy<>));

    private sealed class ServiceLazy<T>(IServiceProvider provider) : Lazy<T>(provider.GetRequiredService<T>) where T : class { }
}
