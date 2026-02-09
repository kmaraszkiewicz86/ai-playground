using AIPlayground.Api.Domain.Interfaces;
using FluentResults;
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
    public async Task<Result<string>> SendPromptAsync(string prompt)
    {
        try
        {
            var content = new StringContent(prompt, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ChatGptEndpoint, content);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("ChatGPT API returned error status {StatusCode}: {Error}", 
                    response.StatusCode, errorContent);
                return Result.Fail($"API request failed with status {response.StatusCode}: {errorContent}");
            }
            
            var responseContent = await response.Content.ReadAsStringAsync();
            return Result.Ok(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending request to ChatGPT API");
            return Result.Fail(new Error("Failed to send request to ChatGPT API").CausedBy(ex));
        }
    }
}
