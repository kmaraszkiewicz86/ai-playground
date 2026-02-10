using AIPlayground.Api.Domain.Enums;

namespace AIPlayground.Api.PresentationLayer.Models;

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
