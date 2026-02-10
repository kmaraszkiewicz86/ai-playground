using AIPlayground.UI.Domain.Enums;

namespace AIPlayground.UI.Domain.Models;

/// <summary>
/// Request model for processing AI questions
/// </summary>
public class ProcessAIQuestionRequest
{
    /// <summary>
    /// The question to ask the AI
    /// </summary>
    public string Question { get; set; } = string.Empty;
    
    /// <summary>
    /// The type of AI service to use
    /// </summary>
    public AiType AiType { get; set; }
}
