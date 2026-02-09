using FluentResults;
using SimpleCqrs;

namespace AIPlayground.UI.Domain.Models
{
    public class GetChatGptAnswerQuery : IQuery<Result<string>>
    {
        public string Prompt { get; set; } = string.Empty;
    }
}
