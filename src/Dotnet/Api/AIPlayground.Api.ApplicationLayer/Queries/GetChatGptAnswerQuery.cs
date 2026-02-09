using AIPlayground.Api.Domain.Interfaces;
using FluentResults;
using SimpleCqrs;

namespace AIPlayground.Api.ApplicationLayer.Queries;

public class GetChatGptAnswerQuery : IQuery<Result<string>>
{
    public string Prompt { get; set; } = string.Empty;
}

public class GetChatGptAnswerQueryHandler : IQueryHandler<GetChatGptAnswerQuery, Result<string>>
{
    private readonly IChatGptHttpService _service;

    public GetChatGptAnswerQueryHandler(IChatGptHttpService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task<Result<string>> Handle(GetChatGptAnswerQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.Prompt))
        {
            return Result.Fail<string>("Prompt cannot be empty");
        }

        return await _service.SendPromptAsync(query.Prompt);
    }
}
