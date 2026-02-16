using AIPlayground.Api.Configuration;
using AIPlayground.Api.PresentationLayer.Endpoints;

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

app.MapAIEndpoints();

app.Run();

