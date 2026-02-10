using AIPlayground.UI.PresentationLayer.ViewModels;
using AIPlayground.UI.PresentationLayer.Views;

namespace AIPlayground.UI.PresentationLayer.Extensions;

/// <summary>
/// Extension methods for configuring services in the DI container
/// </summary>
public static class ServiceCollectionExtensions
{
    private const int DefaultTimeoutSeconds = 30;

    /// <summary>
    /// Adds ChatGPT HTTP client services to the service collection
    /// </summary>
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<AIDemoListViewModel>();
        services.AddTransient<ChatGptPromptViewModel>();

        return services;
    }

    /// <summary>
    /// Adds all UI services to the service collection
    /// </summary>
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<AIDemoList>();
        services.AddTransient<ChatGptPrompt>();

        return services;
    }
}
