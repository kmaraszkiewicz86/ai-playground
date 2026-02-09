using AIPlayground.UI.Domain.Interfaces;

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

    /// <inheritdoc/>
    public async Task<string> SendPromptAsync(string prompt)
    {
        var httpContent = new StringContent(prompt, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(ChatGptEndpoint, httpContent);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
