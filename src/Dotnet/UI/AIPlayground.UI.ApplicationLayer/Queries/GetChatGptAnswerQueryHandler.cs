using AIPlayground.UI.Domain.Interfaces;
using AIPlayground.UI.Domain.Models;
using FluentResults;
using SimpleCqrs;

namespace AIPlayground.UI.ApplicationLayer.Queries;

public class GetChatGptAnswerQueryHandler : IAsyncQueryHandler<GetChatGptAnswerQuery, Result<string>>
{
    private readonly IChatGptHttpService _service;

    public GetChatGptAnswerQueryHandler(IChatGptHttpService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task<Result<string>> HandleAsync(GetChatGptAnswerQuery query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.Prompt))
        {
            return Result.Fail<string>("Prompt cannot be empty");
        }

        return await _service.SendPromptAsync(query.Prompt);
    }
}
