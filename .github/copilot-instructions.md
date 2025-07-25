# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

maERP is a C# client-server, cross-platform, open-source ERP system developed with .NET 9, Avalonia and Entity Framework. It follows the Clean Architecture with a clear separation of concerns. It consists of the projects:

1. **maERP.Domain** - Core domain entities and interfaces
2. **maERP.Application** - Application logic, CQRS handlers
3. **maERP.Infrastructure** - Cross-cutting concerns (email, logging, PDF generation)
4. **maERP.Persistence** - Database access, repositories
5. **maERP.Identity** - Authentication and authorization
6. **maERP.SalesChannels** - Integrations with e-commerce platforms
7. **maERP.Server** - Backend API server (headless, no frontend)
8. **maERP.UI** - Shared UI components for use in Avalonia applications
9. **maERP.UI.Desktop** - Avalonia Desktop Client
10. **maERP.UI.Browser** - Avalonia WebAssembly Client
11. **maERP.UI.iOS** - Aavalonia iOS App
12. **maERP.UI.Android** - Avalonia Android App

## Architecture

The codebase implements:
- CQRS pattern for separating commands and queries
- Repository pattern for data access
- JWT authentication
- No Automapper, using manual mapping instead
- Avalonia with CommunityToolkit.MVVM for cross-platform UI
- UI projects not using direct database acces, using REST-API instead

## Development Commands

### Building the Project

```bash
# Build the entire solution
dotnet build

# Build maERP.Server project
dotnet build src/maERP.Server/maERP.Server.csproj

# Build maERP.UI.Browser project
dotnet build src/maERP.UI.Browser/maERP.UI.Browser.csproj

```

### Running the Application

```bash
# Run the server
dotnet run --project src/maERP.Server/maERP.Server.csproj

# Run the web frontend
dotnet run --project src/maERP.UI.Browser/maERP.UI.Browser.csproj

# Run the multi-platform client
dotnet run --project src/maERP.UI.Desktop/maERP.UI.Desktop.csproj
```

### Testing

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/maERP.Server.Tests/maERP.Server.Tests.csproj

# Run specific test class
dotnet test tests/maERP.Server.Tests/maERP.Server.Tests.csproj --filter "FullyQualifiedName~CustomerCrudTest"

# Run specific test method
dotnet test tests/maERP.Server.Tests/maERP.Server.Tests.csproj --filter "FullyQualifiedName~CustomerCrudTest.CustomerCreateTest"
```

### Database Migrations

The project supports multiple database providers (MySQL, PostgreSQL, MSSQL) with separate migration assemblies:

```bash
# Create migrations for all providers
./create-migrations.sh "MigrationName"

# Create migrations for specific provider
./create-migrations.sh "MigrationName" mysql
./create-migrations.sh "MigrationName" mssql
./create-migrations.sh "MigrationName" postgresql
```

### Code Style and Quality

```bash
# Run code format check
dotnet format --verify-no-changes

# Apply code formatting
dotnet format
```

## Important Notes

- All comments should be in English
- The project uses dependency injection heavily throughout all layers
- Database provider can be configured in appsettings.json or environment variables
- Docker containerization is fully supported and recommended for deployment
- Authentication is JWT-based
- maERP.Server is built with .NET 9 ASP.NET Core
- maERP.Server uses MediatR for CQRS pattern
- The project uses Entity Framework Core for database access
- The project uses C# 10+ features when appropriate
- The project uses FluentValidation for validation
- The project uses Serilog for logging
- The project uses GitHub Actions for CI/CD
- maERP.UI is not executable. It is a shared library for maERP.Browser, maERP.Desktop, maERP.iOS and maERP.Android
- Avalonia is used for cross-platform UI development
- Avalonia is using CommunityToolkit.MVVM and CompiledBindings
- ViewModels are registered in maERP.UI/app.xaml.cs
- DTOs are defined in maERP.Domain an available as ListDto, DetailDto and InputDto
- Repositories are defined in maERP.Persistence
- Services are defined in maERP.Application
- on layout changes, always consider the Avalonia platform limitations and capabilities
- when implementing new features, always consider the cross-platform nature of the project
- when implementing new features, always consider the performance and scalability of the solution
- when implementing new features, always consider the security implications of the solution
- when adding new axaml files, proof if the DataTemplate neeed to be added to MainView.axaml
- IMPORTANT: when implementing new layouts, always heavily think about the user experience and usability 
- IMPORTANT: on layout changes, always look if there is a similar layout and write consistent code
- When implementing new features or functions, YOU MUST look if there is a similar feature or function and write consistent code