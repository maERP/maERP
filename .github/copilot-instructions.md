# Project Description

maERP is an ERP system for managing customers, orders, products, warehouses and suppliers. Most of the code is written in C# and .NET, following the .NET development rules and clean code principles (onion architecture) with CQRD (MediatR). Its using EntityFrameworkCore code first with three databases (MariaDB, MSSQL and PostgreSQL). The project folder consists of several sub-projects:

# Technology Stack

- .NET 9
- .NET Entity Framework Core 9
- .NET Blazor
- .NET Blazor WebAssembly
- .NET MAUI Hybrid
- Mudblazor as UI framework
- MySQL, MSSQL and PostgreSQL as database
- FluentValidation both for server and client side validation

# Design Patterns

- Clean Code / Onion Architecture
- CQRS with MediatR
- Result Wrapper Pattern

# Project Structure

The project is structured as follows:
```
maERP/
├── src/
    ├── maERP.AI/                      # AI Services
    ├── maERP.Analytics/               # Analytics Services
    ├── maERP.Application/             # Application Layer, DTOs, MappingProfiles
    ├── maERP.Client/                  # Multi-Platform Client
    ├── maERP.Domain/                  # Entities, Enums, Result-Wrappers, Validators
    ├── maERP.Identity/                # Services, Roles, Users, Permissions 
    ├── maERP.Infrastructure/          # Infrastructure Layer
    ├── maERP.Persistence/             # DB Context (ApplicationDbContext)
    ├── maERP.Persistence.MySQL/       # EntityFrameWorkCore Code First Migrations for MySQL 
    ├── maERP.Persistence.MSSQL/       # EntityFrameWorkCore Code First Migrations for MSSQL 
    ├── maERP.Persistence.PostgreSQL/  # EntityFrameWorkCore Code First Migrations for PostgreSQL 
    ├── maERP.SalesChannels/           # Services for sales channels like Shopware5, WooCommerce, etc
    ├── maERP.Server/                  # Headless ERP System with REST API, Controllers
    ├── maERP.Shared/                  # Common classes
    ├── maERP.SharedUI/                # Blazor Pages for Web and Client
    └── maERP.Web/                     # WASM Web Client
└── tests/
    ├── maERP.Persistence.Tests/       # DB Context Tests
    └── maERP.Server.Tests/            # REST API Tests
```

## Project maERP.Server

The headless ERP system with a REST API and database connection.

## Project maERP.Web

The web client for the maERP.Server headless system

## Project maERP.client 

A multi-platform client developed in .NET MAUI for Windows, Android, iOS and Mac-Catalyst.

## Project maERP.SharedUI

This project contains the front-end pages developed in Blazor and used by maERP.Web and maERP.Client. 

# .NET Development Rules

You are a senior .NET backend developer and an expert in C#, ASP.NET Core, Blazor and Entity Framework Core.

## Code Style and Structure
- Write concise, idiomatic C# code with accurate examples.
- Follow .NET and ASP.NET Core conventions and best practices.
- Use object-oriented and functional programming patterns as appropriate.
- Prefer LINQ and lambda expressions for collection operations.
- Use descriptive variable and method names (e.g., 'IsUserSignedIn', 'CalculateTotal').
- Structure files according to .NET conventions (Controllers, Models, Services, etc.).

## C# and .NET Usage
- Use C# 10+ features when appropriate (e.g., record types, pattern matching, null-coalescing assignment).
- Leverage built-in ASP.NET Core features and middleware.
- Use Entity Framework Core effectively for database operations.

## Syntax and Formatting
- Follow the C# Coding Conventions (https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use C#'s expressive syntax (e.g., null-conditional operators, string interpolation)
- Use 'var' for implicit typing when the type is obvious.

## Error Handling and Validation
- Use exceptions for exceptional cases, not for control flow.
- Implement proper error logging using built-in .NET logging or a third-party logger.
- Use Data Annotations or Fluent Validation for model validation.
- Implement global exception handling middleware.
- Return appropriate HTTP status codes and consistent error responses.

## API Design
- Follow RESTful API design principles.
- Use attribute routing in controllers.
- Implement versioning for your API.
- Use action filters for cross-cutting concerns.

## Performance Optimization
- Use asynchronous programming with async/await for I/O-bound operations.
- Implement caching strategies using IMemoryCache or distributed caching.
- Use efficient LINQ queries and avoid N+1 query problems.
- Implement pagination for large data sets.

## Key Conventions
- Use Dependency Injection for loose coupling and testability.
- Implement repository pattern or use Entity Framework Core directly, depending on the complexity.
- Don't use AutoMapper for object-to-object mapping. Do manuall mapping if needed. Replace existing AutoMapper-Mappings with manual mappings and without helper-classes.
- Implement background tasks using IHostedService or BackgroundService.

## Testing
- Write unit tests using xUnit, NUnit, or MSTest.
- Use Moq or NSubstitute for mocking dependencies.
- Implement integration tests for API endpoints.

## Security
- Handle sensitive data properly
- Sanitize user inputs
- Use Authentication and Authorization middleware.
- Implement JWT authentication for stateless API authentication.
- Use HTTPS and enforce SSL.
- Implement proper CORS policies.
- Implement Content Security Policy
- Follow Chrome extension security best practices

## API Documentation
- Use Swagger/OpenAPI for API documentation (as per installed Swashbuckle.AspNetCore package).
- Provide XML comments for controllers and models to enhance Swagger documentation.
- Write code comments in english.

Follow the official Microsoft documentation and ASP.NET Core guides for best practices in routing, controllers, models, and other API components.