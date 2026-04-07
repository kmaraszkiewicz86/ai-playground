using FluentResults;
using SimpleCqrs;

namespace AIPlayground.Api.ApplicationLayer.Queries.GetChatGptAnswerQuery;

public class GetChatGptAnswerQuery : IQuery<Result<string>>
{
    public string Prompt { get; set; } = string.Empty;
}
