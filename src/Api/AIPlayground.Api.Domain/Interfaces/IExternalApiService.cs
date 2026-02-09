namespace AIPlayground.Api.Domain.Interfaces;

/// <summary>
/// Interface for communicating with external APIs like ChatGPT
/// </summary>
public interface IExternalApiService
{
    /// <summary>
    /// Sends a request to an external API
    /// </summary>
    Task<string> SendRequestAsync(string endpoint, string payload);
}
