using AIPlayground.UI.ApplicationLayer.Queries;
using AIPlayground.UI.Domain.Interfaces;
using AIPlayground.UI.Infrastructure.Services;
using SimpleCqrs;
using Microsoft.Extensions.DependencyInjection;

namespace AIPlayground.UI.Configuration;

/// <summary>
/// Extension methods for configuring services in the DI container
/// </summary>
public static class ServiceCollectionExtensions
{
    private const int DefaultTimeoutSeconds = 30;

    /// <summary>
    /// Adds ChatGPT HTTP client services to the service collection
    /// </summary>
    public static IServiceCollection AddChatGptHttpServices(this IServiceCollection services)
    {
        string baseAddress = "https://api.openai.com/v1/"; // Base URL for OpenAI API

        // Register HttpClient factory for ChatGPT
        services.AddHttpClient<IChatGptHttpService, ChatGptHttpService>(client =>
        {
            client.BaseAddress = new Uri(baseAddress);
            client.Timeout = TimeSpan.FromSeconds(DefaultTimeoutSeconds);
        });

        return services;
    }

    /// <summary>
    /// Adds all UI services to the service collection
    /// </summary>
    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        // Configure SimpleCqrs
        services.ConfigureSimpleCqrs(typeof(GetChatGptAnswerQueryHandler).Assembly);
        
        return services;
    }
}
