using AIPlayground.Api.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AIPlayground.Api.Infrastructure.Services;

/// <summary>
/// Service for communicating with ChatGPT API
/// </summary>
public class ChatGptHttpService : IChatGptHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ChatGptHttpService> _logger;
    private const string ChatGptEndpoint = "/v1/chat/completions";

    public ChatGptHttpService(HttpClient httpClient, ILogger<ChatGptHttpService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public async Task<string> SendPromptAsync(string prompt)
    {
        try
        {
            var content = new StringContent(prompt, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ChatGptEndpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending request to ChatGPT API");
            throw;
        }
    }
}
