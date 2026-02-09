namespace AIPlayground.UI.Domain.Interfaces;

/// <summary>
/// Interface for HTTP service operations
/// </summary>
public interface IHttpService
{
    /// <summary>
    /// Gets data from the specified endpoint
    /// </summary>
    Task<string> GetAsync(string endpoint);

    /// <summary>
    /// Posts data to the specified endpoint
    /// </summary>
    Task<string> PostAsync(string endpoint, string content);
}
