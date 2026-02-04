namespace WebViewCore.Ioc;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

internal static class WebViewLocator
{
    private static IServiceCollection serviceCollection = new ServiceCollection();
    //private static IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
    private static Lazy<IServiceProvider> serviceProviderLazy = new Lazy<IServiceProvider>(() => serviceCollection.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true }));

    public static void RegisterSingleton<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>() where TService : class
    {
        serviceCollection.AddSingleton<TService>();
    }

    public static void RegisterSingleton<TService>(TService service) where TService : class
    {
        serviceCollection.AddSingleton<TService>(service);
    }

    public static void RegisterSingleton<TService>(Func<TService> func) where TService : class
    {
        serviceCollection.AddSingleton<TService>(m => func.Invoke());
    }

    public static void RegisterSingleton<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>() where TService : class where TImplementation : class, TService
    {
        serviceCollection.AddSingleton<TService, TImplementation>();
    }

    public static TService? ResolveInstance<TService>() where TService : class
    {
        return serviceProviderLazy.Value.GetService<TService>();
    }

    public static TService ResolveRequiredInstance<TService>() where TService : class
    {
        return serviceProviderLazy.Value.GetRequiredService<TService>();
    }
}
