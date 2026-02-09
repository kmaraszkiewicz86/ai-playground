using AIPlayground.Api.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AIPlayground.Api.Infrastructure.Services;

/// <summary>
/// Service for communicating with external APIs like ChatGPT
/// </summary>
public class ExternalApiService : IExternalApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ExternalApiService> _logger;

    public ExternalApiService(IHttpClientFactory httpClientFactory, ILogger<ExternalApiService> logger)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public async Task<string> SendRequestAsync(string endpoint, string payload)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient("ExternalApiClient");
            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending request to external API");
            throw;
        }
    }
}
