using AIPlayground.Api.ApplicationLayer.Queries.GetChatGptAnswerQuery;
using AIPlayground.Api.Domain.Interfaces;
using AIPlayground.Api.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using SimpleCqrs;
using System.Net.Http.Headers;

namespace AIPlayground.Api.Configuration;

/// <summary>
/// Extension methods for configuring API services in the DI container
/// </summary>
public static class ServiceCollectionExtensions
{
    private const int DefaultTimeoutSeconds = 30;

    /// <summary>
    /// Adds ChatGPT HTTP client services to the service collection
    /// </summary>
    public static IServiceCollection AddChatGptHttpServices(this IServiceCollection services, string baseAddress, int timeoutSeconds = DefaultTimeoutSeconds)
    {
        // Register HttpClient factory for ChatGPT
        services.AddHttpClient<IChatGptHttpService, ChatGptHttpService>(client =>
        {
            client.BaseAddress = new Uri(baseAddress);
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", "***");

        });

        return services;
    }

    /// <summary>
    /// Adds all API services to the service collection
    /// </summary>
    public static IServiceCollection AddApiServices(this IServiceCollection services, string chatGptApiBaseAddress, int timeoutSeconds = DefaultTimeoutSeconds)
    {
        services.AddChatGptHttpServices(chatGptApiBaseAddress, timeoutSeconds);
        
        // Configure SimpleCqrs
        services.ConfigureSimpleCqrs(typeof(GetChatGptAnswerQueryHandler).Assembly);
        
        return services;
    }
}
