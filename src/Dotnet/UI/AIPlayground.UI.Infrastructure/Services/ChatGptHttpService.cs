using AIPlayground.UI.Domain.Enums;
using AIPlayground.UI.Domain.Interfaces;
using AIPlayground.UI.Domain.Models;
using FluentResults;
using System.Text.Json;

namespace AIPlayground.UI.Infrastructure.Services;

/// <summary>
/// ChatGPT HTTP service implementation using HttpClient to call the API
/// </summary>
public class ChatGptHttpService : IChatGptHttpService
{
    private readonly HttpClient _httpClient;
    private const string ProcessAIQuestionEndpoint = "/api/processAIQuestionAsync";
    private static readonly JsonSerializerOptions JsonOptions = new() 
    { 
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public ChatGptHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Result<string>> SendPromptAsync(string prompt)
    {
        try
        {
            var request = new ProcessAIQuestionRequest
            {
                Question = prompt,
                AiType = AiType.ChatGpt
            };
            
            var jsonContent = JsonSerializer.Serialize(request, JsonOptions);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ProcessAIQuestionEndpoint, httpContent);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail($"API request failed with status {response.StatusCode}: {responseContent}");
            }
            
            var apiResponse = JsonSerializer.Deserialize<ProcessAIQuestionResponse>(responseContent, JsonOptions);
            
            if (apiResponse == null)
            {
                return Result.Fail("Failed to deserialize API response");
            }
            
            if (!apiResponse.IsSuccess)
            {
                return Result.Fail(string.Join(", ", apiResponse.Errors));
            }
            
            return Result.Ok(apiResponse.Answer ?? string.Empty);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to send request to API").CausedBy(ex));
        }
    }
}
