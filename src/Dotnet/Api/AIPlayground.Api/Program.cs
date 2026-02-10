using AIPlayground.Api.ApplicationLayer.Queries;
using AIPlayground.Api.Configuration;
using AIPlayground.Api.PresentationLayer.Models;
using SimpleCqrs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Get ChatGPT API configuration
var chatGptApiBaseAddress = builder.Configuration.GetValue<string>("ChatGptApi:BaseAddress", "https://api.openai.com");
var httpClientTimeoutSeconds = builder.Configuration.GetValue<int>("HttpClient:TimeoutSeconds", 30);

// Register services using Configuration layer
builder.Services.AddApiServices(chatGptApiBaseAddress, httpClientTimeoutSeconds);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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

app.Run();
