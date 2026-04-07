using AIPlayground.Api.Domain.Interfaces;
using FluentResults;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

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
            // 1. Prepare the JSON payload following the OpenAI chat completions schema
            // LLMs require a list of messages to maintain context
            var requestBody = new
            {
                model = "gpt-4o", // You can use gpt-4o, gpt-3.5-turbo, etc.
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var jsonPayload = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            // 2. Send the POST request to the ChatGptEndpoint
            var response = await _httpClient.PostAsync(ChatGptEndpoint, content);

            // 3. Handle unsuccessful status codes
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("ChatGPT API returned error status {StatusCode}: {Error}",
                    response.StatusCode, errorContent);

                return Result.Fail($"API request failed with status {response.StatusCode}: {errorContent}");
            }

            // 4. Read and return the raw JSON response
            var responseContent = await response.Content.ReadAsStringAsync();
            return Result.Ok(responseContent);
        }
        catch (Exception ex)
        {
            // 5. Log exception and wrap it in a Result object
            _logger.LogError(ex, "Error sending request to ChatGPT API");
            return Result.Fail(new Error("Failed to send request to ChatGPT API").CausedBy(ex));
        }
    }
}
