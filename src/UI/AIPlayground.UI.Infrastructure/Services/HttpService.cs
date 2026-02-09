using AIPlayground.UI.Domain.Interfaces;

namespace AIPlayground.UI.Infrastructure.Services;

/// <summary>
/// HTTP service implementation using IHttpClientFactory
/// </summary>
public class HttpService : IHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <inheritdoc/>
    public async Task<string> GetAsync(string endpoint)
    {
        using var client = _httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <inheritdoc/>
    public async Task<string> PostAsync(string endpoint, string content)
    {
        using var client = _httpClientFactory.CreateClient("ApiClient");
        var httpContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(endpoint, httpContent);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
