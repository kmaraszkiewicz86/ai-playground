using AIPlayground.UI.Domain.Interfaces;
using AIPlayground.UI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AIPlayground.UI.Configuration;

/// <summary>
/// Extension methods for configuring services in the DI container
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds HTTP client services to the service collection
    /// </summary>
    public static IServiceCollection AddHttpServices(this IServiceCollection services, string baseAddress)
    {
        // Register HttpClient factory
        services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri(baseAddress);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        // Register HTTP service
        services.AddScoped<IHttpService, HttpService>();

        return services;
    }

    /// <summary>
    /// Adds all UI services to the service collection
    /// </summary>
    public static IServiceCollection AddUIServices(this IServiceCollection services, string apiBaseAddress)
    {
        services.AddHttpServices(apiBaseAddress);
        return services;
    }
}
