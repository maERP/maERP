# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

maERP is a client-server, cross-platform, open-source ERP system developed with .NET 9, MAUI and Entity Framework. It consists of three main projects:

1. **maERP.Server** - Backend API server (headless, no frontend)
2. **maERP.Web** - Web frontend connecting to maERP.Server
3. **maERP.Client** - Native client apps (iOS, Android, Windows, macOS)

## Architecture

maERP follows Clean Architecture with a clear separation of concerns:

- **Domain Layer** (`maERP.Domain`) - Core entities, DTOs, interfaces
- **Application Layer** (`maERP.Application`) - Business logic, CQRS handlers using MediatR
- **Infrastructure** (`maERP.Infrastructure`) - Cross-cutting concerns (email, logging, PDF)
- **Persistence** (`maERP.Persistence`) - Database access, repositories
- **Identity** (`maERP.Identity`) - Authentication/authorization
- **SalesChannels** (`maERP.SalesChannels`) - Integrations with e-commerce platforms
- **UI Layers** (`maERP.Web`, `maERP.Client`, `maERP.SharedUI`) - User interfaces


The codebase implements:
- CQRS pattern for separating commands and queries
- Repository pattern for data access
- JWT authentication

## Development Commands

### Building the Project

```bash
# Build the entire solution
dotnet build

# Build specific project
dotnet build src/maERP.Server/maERP.Server.csproj
```

### Running the Application

```bash
# Run the server
dotnet run --project src/maERP.Server/maERP.Server.csproj

# Run the web frontend
dotnet run --project src/maERP.Web/maERP.Web.csproj

# Run the multi-platform client
dotnet run --project src/maERP.Client/maERP.Client.csproj
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

- The project uses dependency injection heavily throughout all layers
- Database provider can be configured in appsettings.json or environment variables
- Docker containerization is fully supported and recommended for deployment
- Authentication is JWT-based
- The UI is built with Blazor and shared components in maERP.SharedUI
- The server is built with .NET 9 ASP.NET Core
- The client is built with .NET 9 and MAU Hybrid UI
- The project uses Entity Framework Core for database access
- The project uses MediatR for CQRS pattern
- The project uses C# 10+ features when appropriate
- The project uses FluentValidation for validation
- The project uses Serilog for logging
- The project uses GitHub Actions for CI/CD
- The project don't use AutoMapper