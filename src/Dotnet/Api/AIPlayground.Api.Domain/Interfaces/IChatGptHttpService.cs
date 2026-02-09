namespace AIPlayground.Api.Domain.Interfaces;

/// <summary>
/// Interface for communicating with ChatGPT API
/// </summary>
public interface IChatGptHttpService
{
    /// <summary>
    /// Sends a prompt to ChatGPT and gets the response
    /// </summary>
    Task<string> SendPromptAsync(string prompt);
}
