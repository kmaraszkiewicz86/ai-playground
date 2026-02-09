using AIPlayground.UI.Domain.Interfaces;
using FluentResults;

namespace AIPlayground.UI.Infrastructure.Services;

/// <summary>
/// ChatGPT HTTP service implementation using HttpClient
/// </summary>
public class ChatGptHttpService : IChatGptHttpService
{
    private readonly HttpClient _httpClient;
    private const string ChatGptEndpoint = "/v1/chat/completions";

    public ChatGptHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Result<string>> SendPromptAsync(string prompt)
    {
        try
        {
            var httpContent = new StringContent(prompt, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ChatGptEndpoint, httpContent);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return Result.Fail($"API request failed with status {response.StatusCode}: {errorContent}");
            }
            
            var responseContent = await response.Content.ReadAsStringAsync();
            return Result.Ok(responseContent);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to send request to ChatGPT API").CausedBy(ex));
        }
    }
}
