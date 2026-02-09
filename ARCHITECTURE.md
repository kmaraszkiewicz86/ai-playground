# AIPlayground Architecture

## Overview

AIPlayground is a .NET-based solution demonstrating Domain-Driven Design (DDD) principles with both UI and API components. The project is designed to facilitate learning and experimentation with AI concepts using .NET and Python integration.

## Solution Structure

```
AIPlayground/
├── src/
│   ├── UI/                           # User Interface Projects
│   │   ├── PresentationLayer/        # UI Resources and Localization
│   │   ├── Domain/                   # Core Business Logic & Interfaces
│   │   ├── ApplicationLayer/         # Application Use Cases
│   │   ├── Infrastructure/           # External Service Implementations
│   │   └── Configuration/            # DI Configuration
│   └── Api/                          # API Projects
│       ├── AIPlayground.Api/         # ASP.NET Core Minimal API
│       ├── Api.Domain/               # API Core Domain
│       ├── Api.ApplicationLayer/     # API Application Logic
│       ├── Api.Infrastructure/       # API Infrastructure
│       └── PythonApi/                # Python HTTP Server
└── AIPlayground.slnx                 # Solution File
```

## Architectural Patterns

### Domain-Driven Design (DDD)

The solution follows DDD principles with clear separation of concerns:

#### 1. Domain Layer
- Contains business logic and domain models
- Defines interfaces for infrastructure dependencies
- No dependencies on other layers
- Examples:
  - `IHttpService` - HTTP communication interface
  - `IExternalApiService` - External API integration interface

#### 2. Application Layer
- Implements use cases and application logic
- Orchestrates domain objects
- Depends only on Domain layer

#### 3. Infrastructure Layer
- Implements domain interfaces
- Handles external dependencies (HTTP, databases, etc.)
- Examples:
  - `HttpService` - IHttpClientFactory-based HTTP service
  - `ExternalApiService` - External API communication service

#### 4. Presentation Layer
- UI resources and components
- Localization resources (Translator.resx)

#### 5. Configuration Layer
- Dependency Injection setup
- Service registration extensions

### Dependency Injection

All services are configured through extension methods:

```csharp
// UI Services
services.AddUIServices("https://api-base-url", timeoutSeconds: 30);

// API Services (configured automatically via Program.cs)
```

### HTTP Client Pattern

Both UI and API projects use `IHttpClientFactory` for HTTP communication:

- Named clients for different purposes
- Configurable timeouts via parameters/configuration
- Proper resource management
- Resilient HTTP calls

## Technology Stack

### .NET Components
- **.NET 10.0**: Core framework
- **ASP.NET Core Minimal API**: Lightweight API framework
- **IHttpClientFactory**: HTTP client management
- **Microsoft.Extensions.DependencyInjection**: DI container

### Python Components
- **Python 3.12**: Python runtime
- **http.server**: Built-in HTTP server module

## Configuration

### API Configuration (appsettings.json)

```json
{
  "HttpClient": {
    "TimeoutSeconds": 30
  }
}
```

### UI Configuration

```csharp
services.AddUIServices(
    apiBaseAddress: "https://api.example.com",
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
4. **Resource Management**: Proper use of IHttpClientFactory
5. **Logging**: Structured logging in infrastructure services
6. **Error Handling**: Exception handling in service implementations
7. **Code Quality**: No magic numbers, all extracted to configuration

## Future Enhancements

Potential areas for expansion:
- Add actual MAUI UI application consuming the services
- Implement ChatGPT API integration
- Add authentication/authorization
- Implement CQRS pattern in Application layer
- Add unit and integration tests
- Add API versioning
- Implement retry policies with Polly
