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
- No MediatR, using manual Mediator instead
- Avalonia with CommunityToolkit.MVVM for cross-platform UI
- UI projects not using direct database access, using REST-API instead

## Role Management
- IMPORTANT: only Superadmin-Role can use SuperadminController to add, edit or delete users in any tenant
- IMPORTANT: all users can see their own user profile, even when they have no UserTenant
- IMPORTANT: all users only see data from their own tenants
- IMPORTANT: With RoleManageUser-Role, a user can manage add or delete users in their own tenant via SuperadminController
- IMPORTANT: With RoleManageTenant-Role, a user edit or delete tenants their own tenant via TenantsController
- Login is possible without having a tenant. Every User can create Tenants. Every user can only see their own tenants.

## Development Commands

### Available MCP Server
- jetbrains for general, debugging, error, files, formatting, text and version control operations
- ref for getting official documentations
- postgres for database operations

Use MCP Server if needed. Do not use jetbrains for shell commands.

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

#### Multi-Tenant Testing Infrastructure

The project uses a specialized testing infrastructure for multi-tenant scenarios:

**TenantIsolatedTestBase**: All multi-tenant tests should inherit from `TenantIsolatedTestBase` instead of implementing their own test setup. This base class provides:

- **Automatic test isolation**: Each test gets its own database and tenant context
- **Built-in tenant management**: Helper methods for setting tenant headers and simulating different scenarios
- **Proper authentication simulation**: Support for authenticated/unauthenticated requests

**Usage Example**:
```csharp
public class MyTenantAwareTests : TenantIsolatedTestBase
{
    [Fact]
    public async Task MyTest_WithTenant1_ShouldReturnTenant1Data()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/MyResource");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        // ... additional assertions
    }
}
```

**Available Helper Methods**:
- `SetTenantHeader(Guid tenantId)` - Set valid tenant header
- `SetInvalidTenantHeader()` - Set non-existent but valid GUID tenant header
- `SetInvalidTenantHeaderValue(string value)` - Set invalid header format
- `RemoveTenantHeader()` - Remove tenant header entirely
- `SimulateUnauthenticatedRequest()` - Make request unauthenticated
- `SimulateAuthenticatedRequest()` - Make request authenticated
- `PostAsJsonAsync<T>()`, `PutAsJsonAsync<T>()` - HTTP helper methods
- `ReadResponseAsync<T>()`, `ReadResponseStringAsync()` - Response parsing helpers

**Testing Scenarios**:
- Tenant data isolation (each tenant sees only their data)
- Cross-tenant access prevention
- Missing/invalid tenant header handling
- Authentication and authorization with tenants
- Unauthenticated request handling

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
- The project is multi tenancy enabled
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
- IMPORTANT: data isolation of tenants must always be ensured. In most cases with global EF Core query filters.
- IMPORTANT: when implementing new layouts, always heavily think about the user experience and usability
- IMPORTANT: on layout changes, always look if there is a similar layout and write consistent code
- When implementing new features or functions, YOU MUST look if there is a similar feature or function and write consistent code
- Tests are using own Factory-Instances instead of shared Fixtures
- Don't use FluentAssertions
- Use RFC 7807 for problem details
- Important: When fixing tests, first check whether the logic of the test is correct. If it is correct, fix the code of the program.
- TenantId is type Guid
- StrictEnumConverter.cs is used for safe enum serialization
- Use GlobalExceptionFilters
- IMPORTANT: cascade deletes must be implemented in the handler or repository
- Pagination is zero-based and defined in QueryableExtensions.cs
- IMPORTANT: all Entities are using System.Guid for Id, defined in BaseEntity.cs
- IMPORTANT: never create db migrations before asking
- EnsureSuperadminAccessAsync() is only for running Tests
- Avalonia can't use dynamic resources in thickness attributes like padding, margin or spacing

# Multi-Tenant Testing Guidelines
- IMPORTANT: All multi-tenant tests MUST inherit from TenantIsolatedTestBase for proper isolation
- IMPORTANT: Use TestDataSeeder.SeedTestDataAsync() to populate test data across tenants
- IMPORTANT: Always test tenant isolation scenarios - verify each tenant only sees their own data
- IMPORTANT: Test both authenticated and unauthenticated scenarios using helper methods
- IMPORTANT: Verify proper HTTP status codes for missing/invalid tenant headers (Unauthorized for invalid format, NotFound for valid but non-existent tenant)
- IMPORTANT: When creating new multi-tenant features, add comprehensive tests covering all tenant scenarios