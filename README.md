# AIPlayground

A .NET MAUI application with a lightweight API, designed for learning and experimenting with AI concepts using .NET and Python integration.

## Project Structure

The solution follows Domain-Driven Design (DDD) principles and is organized into two main sections:

### UI Projects (src/UI/)
- **AIPlayground.UI.PresentationLayer**: .NET class library with Translator resource for UI localization
- **AIPlayground.UI.Domain**: Core domain layer containing interfaces (IHttpService)
- **AIPlayground.UI.ApplicationLayer**: Application logic and use cases
- **AIPlayground.UI.Infrastructure**: Infrastructure implementations including HttpService
- **AIPlayground.UI.Configuration**: Dependency injection configuration with ServiceCollectionExtensions

### API Projects (src/Api/)
- **AIPlayground.Api**: ASP.NET Core Minimal API with DDD layers
  - Simple GET endpoint at `/api/hello`
  - Support for external API communication (e.g., ChatGPT API)
- **AIPlayground.Api.Domain**: Domain interfaces (IExternalApiService)
- **AIPlayground.Api.ApplicationLayer**: Application logic layer
- **AIPlayground.Api.Infrastructure**: Infrastructure implementations including ExternalApiService
- **PythonApi**: Lightweight Python HTTP server

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
cd src/Api/AIPlayground.Api
dotnet run
```

The API will be available at http://localhost:5238

### Running the Python API

```bash
cd src/Api/PythonApi
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
- `GET /api/hello` - Returns a hello world message from Python API

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
- **Domain**: Contains business logic and interfaces
- **Infrastructure**: Implements domain interfaces using IHttpClientFactory
- **Configuration**: Configures dependency injection for services
- **PresentationLayer**: Contains UI resources and translations

### API Architecture
The API uses minimal API pattern with DDD layers:
- **Domain**: Defines contracts for external services
- **Infrastructure**: Implements external API communication
- **API Project**: Hosts the endpoints and configures services

## HTTP Client Configuration

The UI project uses IHttpClientFactory configured in the Configuration layer:

```csharp
services.AddUIServices("https://api-base-url");
```

This configures:
- Named HttpClient "ApiClient" with base address
- Scoped IHttpService implementation
- 30-second timeout

## License

This project is licensed under the MIT License - see the LICENSE file for details.

