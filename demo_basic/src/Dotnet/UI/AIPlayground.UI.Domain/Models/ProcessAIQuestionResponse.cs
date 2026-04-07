namespace AIPlayground.UI.Domain.Models;

/// <summary>
/// Response model for AI question processing result
/// </summary>
public class ProcessAIQuestionResponse
{
    /// <summary>
    /// Indicates if the operation was successful
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// The answer from the AI service
    /// </summary>
    public string? Answer { get; set; }
    
    /// <summary>
    /// List of error messages if the operation failed
    /// </summary>
    public List<string> Errors { get; set; } = new();
}
