using FluentResults;

namespace AIPlayground.UI.Domain.Interfaces;

/// <summary>
/// Interface for ChatGPT HTTP service operations
/// </summary>
public interface IChatGptHttpService
{
    /// <summary>
    /// Sends a prompt to ChatGPT and gets the response
    /// </summary>
    Task<Result<string>> SendPromptAsync(string prompt);
}
