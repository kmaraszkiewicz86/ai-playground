using AIPlayground.Api.ApplicationLayer.Queries.GetChatGptAnswerQuery;
using AIPlayground.Api.PresentationLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SimpleCqrs;

namespace AIPlayground.Api.PresentationLayer.Endpoints;

public static class AIEndpoints
{
    public static IEndpointRouteBuilder MapAIEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/processAIQuestionAsync", async (ProcessAIQuestionRequest request, ISimpleMediator mediator) =>
        {
            var query = new GetChatGptAnswerQuery { Prompt = request.Question };
            var result = await mediator.GetQueryAsync(query);

            var response = new ProcessAIQuestionResponse
            {
                IsSuccess = result.IsSuccess,
                Answer = result.IsSuccess ? result.Value : null,
                Errors = result.IsFailed ? [.. result.Errors.Select(e => e.Message)] : []
            };

            return result.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        })
        .WithName("ProcessAIQuestionAsync")
        .WithTags("AI");

        return app;
    }
}
