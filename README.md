# AIPlayground

A .NET MAUI application with a lightweight API, designed for learning and experimenting with AI concepts using .NET and Python integration.

## Project Structure

The solution follows Domain-Driven Design (DDD) principles and is organized into language-specific folders:

### .NET Projects (src/Dotnet/)

#### UI Projects (src/Dotnet/UI/)
- **AIPlayground.UI.PresentationLayer**: .NET class library with Translator resource for UI localization
- **AIPlayground.UI.Domain**: Core domain layer containing interfaces (`IChatGptHttpService`)
- **AIPlayground.UI.ApplicationLayer**: Application logic and use cases
- **AIPlayground.UI.Infrastructure**: Infrastructure implementations including `ChatGptHttpService`
- **AIPlayground.UI.Configuration**: Dependency injection configuration with `ServiceCollectionExtensions`

#### API Projects (src/Dotnet/Api/)
- **AIPlayground.Api**: ASP.NET Core Minimal API with DDD layers
  - Simple GET endpoint at `/api/hello`
  - Support for ChatGPT API communication
- **AIPlayground.Api.Domain**: Domain interfaces (`IChatGptHttpService`)
- **AIPlayground.Api.ApplicationLayer**: Application logic layer
- **AIPlayground.Api.Infrastructure**: Infrastructure implementations including `ChatGptHttpService`
- **AIPlayground.Api.Configuration**: DI configuration with `ServiceCollectionExtensions`
- **AIPlayground.Api.PresentationLayer**: API presentation layer

### Python Projects (src/Python/)
- **main.py**: Lightweight Python HTTP server

## Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- Python 3.12 or later

### Building the Solution

```bash
dotnet build
```

### Running the .NET API

```bash
cd src/Dotnet/Api/AIPlayground.Api
dotnet run
```

The API will be available at http://localhost:5238

### Running the Python API

```bash
cd src/Python
python3 main.py
```

The Python API will be available at http://localhost:8001

## API Endpoints

### .NET API
- `GET /api/hello` - Returns a hello message from the .NET API

Example response:
```json
{
    "message": "Hello from AIPlayground API",
    "timestamp": "2026-02-09T11:55:23.4204491Z"
}
```

### Python API
- `GET /api/hello` or `GET /` - Returns a hello world message from Python API

Example response:
```json
{
    "message": "Hello world from Python API",
    "timestamp": "2026-02-09T11:55:42.983279"
}
```

## Architecture

### UI Architecture
The UI follows a clean architecture with clear separation of concerns:
- **Domain**: Contains business logic and interfaces (`IChatGptHttpService`)
- **Infrastructure**: Implements domain interfaces using typed HttpClient
- **Configuration**: Configures dependency injection for services
- **PresentationLayer**: Contains UI resources and translations

### API Architecture
The API uses minimal API pattern with DDD layers:
- **Domain**: Defines contracts for ChatGPT services (`IChatGptHttpService`)
- **Infrastructure**: Implements ChatGPT API communication using HttpClient
- **Configuration**: Configures dependency injection and HttpClient
- **PresentationLayer**: API presentation layer
- **API Project**: Hosts the endpoints and configures services

## HTTP Client Configuration

Both UI and API projects use typed HttpClient configured in the Configuration layer:

```csharp
// UI
services.AddUIServices("https://api.openai.com", timeoutSeconds: 30);

// API
services.AddApiServices("https://api.openai.com", timeoutSeconds: 30);
```

This configures:
- Typed HttpClient injected via DI
- Base address and timeout configuration
- Scoped service lifetime

## License

This project is licensed under the MIT License - see the LICENSE file for details.

