# AIPlayground Architecture

## Overview

AIPlayground is a .NET-based solution demonstrating Domain-Driven Design (DDD) principles with both UI and API components. The project is designed to facilitate learning and experimentation with AI concepts using .NET and Python integration.

## Solution Structure

```
AIPlayground/
├── src/
│   ├── Dotnet/                       # .NET Projects
│   │   ├── UI/                       # User Interface Projects
│   │   │   ├── PresentationLayer/    # UI Resources and Localization
│   │   │   ├── Domain/               # Core Business Logic & Interfaces
│   │   │   ├── ApplicationLayer/     # Application Use Cases
│   │   │   ├── Infrastructure/       # External Service Implementations
│   │   │   └── Configuration/        # DI Configuration
│   │   └── Api/                      # API Projects
│   │       ├── AIPlayground.Api/     # ASP.NET Core Minimal API
│   │       ├── Api.Domain/           # API Core Domain
│   │       ├── Api.ApplicationLayer/ # API Application Logic
│   │       ├── Api.Infrastructure/   # API Infrastructure
│   │       ├── Api.Configuration/    # API DI Configuration
│   │       └── Api.PresentationLayer/# API Presentation
│   └── Python/                       # Python Projects
│       └── main.py                   # Python HTTP Server
└── AIPlayground.sln                  # Solution File
```

## Architectural Patterns

### Domain-Driven Design (DDD)

The solution follows DDD principles with clear separation of concerns:

#### 1. Domain Layer
- Contains business logic and domain models
- Defines interfaces for infrastructure dependencies
- No dependencies on other layers
- Examples:
  - `IChatGptHttpService` - ChatGPT HTTP communication interface

#### 2. Application Layer
- Implements use cases and application logic
- Orchestrates domain objects
- Depends only on Domain layer

#### 3. Infrastructure Layer
- Implements domain interfaces
- Handles external dependencies (HTTP, databases, etc.)
- Examples:
  - `ChatGptHttpService` - HttpClient-based ChatGPT service

#### 4. Presentation Layer
- UI resources and components
- Localization resources (Translator.resx)
- API presentation layer

#### 5. Configuration Layer
- Dependency Injection setup
- Service registration extensions
- HttpClient configuration

### Dependency Injection

All services are configured through extension methods:

```csharp
// UI Services
services.AddUIServices("https://api.openai.com", timeoutSeconds: 30);

// API Services
services.AddApiServices("https://api.openai.com", timeoutSeconds: 30);
```

### HTTP Client Pattern

Both UI and API projects use typed `HttpClient` for HTTP communication:

- Typed clients injected via DI
- Configurable timeouts via parameters/configuration
- Proper resource management
- Resilient HTTP calls

## Technology Stack

### .NET Components
- **.NET 10.0**: Core framework
- **ASP.NET Core Minimal API**: Lightweight API framework
- **HttpClient**: Typed HTTP client management
- **Microsoft.Extensions.DependencyInjection**: DI container

### Python Components
- **Python 3.12**: Python runtime
- **http.server**: Built-in HTTP server module

## Configuration

### API Configuration (appsettings.json)

```json
{
  "ChatGptApi": {
    "BaseAddress": "https://api.openai.com"
  },
  "HttpClient": {
    "TimeoutSeconds": 30
  }
}
```

### UI Configuration

```csharp
services.AddUIServices(
    chatGptApiBaseAddress: "https://api.openai.com",
    timeoutSeconds: 30  // Optional, defaults to 30
);
```

## API Endpoints

### .NET API (Port 5238)
- `GET /api/hello` - Returns greeting message with timestamp

### Python API (Port 8001)
- `GET /api/hello` or `GET /` - Returns hello world message with timestamp

## Best Practices Implemented

1. **Clean Architecture**: Clear separation of concerns with DDD layers
2. **Dependency Inversion**: Domain defines interfaces, infrastructure implements
3. **Configuration over Convention**: Timeout and URLs configurable
4. **Resource Management**: Proper use of typed HttpClient
5. **Logging**: Structured logging in infrastructure services
6. **Error Handling**: Exception handling in service implementations
7. **Code Quality**: No magic numbers, all extracted to configuration
8. **Endpoint Encapsulation**: API endpoints defined in service methods, not as parameters

## Future Enhancements

Potential areas for expansion:
- Add actual MAUI UI application consuming the services
- Implement full ChatGPT API integration
- Add authentication/authorization
- Implement CQRS pattern in Application layer
- Add unit and integration tests
- Add API versioning
- Implement retry policies with Polly
