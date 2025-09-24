# Repository Guidelines

## Project Structure & Module Organization
maERP follows a clean architecture split across `src/`. `maERP.Domain` keeps entities and interfaces, `maERP.Application` provides CQRS handlers via a custom mediator, and `maERP.Infrastructure` plus the `maERP.Persistence*` projects implement cross-cutting services, the repository pattern, and database providers. `maERP.Identity` manages auth, `maERP.SalesChannels` wraps external integrations, `maERP.Server` exposes the REST API, and `maERP.UI.*` projects ship Avalonia clients powered by CommunityToolkit.MVVM. Tests mirror server features under `tests/`, while scripts such as `create-migrations.sh` live beside the solution.

## Build, Test, and Development Commands
- `dotnet restore` – download NuGet dependencies.
- `dotnet build maERP.sln` – compile all targets; CI treats warnings as errors.
- `dotnet run --project src/maERP.Server/maERP.Server.csproj` – launch the API locally.
- `dotnet run --project src/maERP.UI.Desktop/maERP.UI.Desktop.csproj` – open the desktop client.
- `dotnet test` or `dotnet test tests/maERP.Server.Tests/maERP.Server.Tests.csproj --filter "FullyQualifiedName~Invoice"` – run full or targeted xUnit suites.
- `dotnet format --verify-no-changes` (or `dotnet format`) – enforce formatter output before commits.
- `./create-migrations.sh "AddInvoices" postgresql` – scaffold provider-specific EF migrations.

### Available MCP Server
- jetbrains for general, debugging, error, files, formatting, text and version control operations
- ref for getting official documentations
- postgres for database operations

Use MCP Server if needed. Do not use jetbrains for shell commands.

## Coding Style & Naming Conventions
Stick to four-space indentation, file-scoped namespaces, and nullable references. Use PascalCase for types and public members, camelCase for locals, and suffix async methods with `Async`. Mapping stays manual (no Automapper) and mediators are handwritten (no MediatR), so keep handlers explicit and discoverable. Prefer explicit DI registrations over reflection and let `dotnet format` settle spacing and ordering.

## Testing Guidelines
xUnit drives the suite under `tests/maERP.Server.Tests`, organized by feature folders like `Features/Invoice/Commands`. Multi-tenant scenarios must inherit from `TenantIsolatedTestBase` for tenant header helpers, isolated DbContexts, and auth shims. Name classes `<Feature><Action>Tests`; express expectations in method names (e.g., `InvoiceCreate_ShouldReturnCreated`). Use `dotnet test --filter` for focused runs and ensure seeded data is tidied inside fixtures.

## Commit & Pull Request Guidelines
Commits stay concise and imperative (e.g., `Refactor TaxClass validation and controller responses`). Branch names such as `feature/invoice-currency-validation` or `fix/tenant-header` keep context clear. Before opening a PR, run `dotnet build`, `dotnet test`, and `dotnet format --verify-no-changes`, then link issues and capture architecture or UI impacts with screenshots or API samples when relevant.

## Security & Configuration Tips
Avoid committing secrets; configure via `DatabaseConfig__ConnectionStrings__{Provider}` and set `DatabaseConfig__Provider` when switching databases. JWT auth and tenant isolation are enforced server-side—manual requests must include valid tenant headers, and only Superadmin roles may manage users. Docker defaults to MySQL, so adjust env vars for PostgreSQL, MSSQL, or SQLite.

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
- - EnsureSuperadminAccessAsync() is only for running Tests

## Multi-Tenant Testing Guidelines
- IMPORTANT: All multi-tenant tests MUST inherit from TenantIsolatedTestBase for proper isolation
- IMPORTANT: Use TestDataSeeder.SeedTestDataAsync() to populate test data across tenants
- IMPORTANT: Always test tenant isolation scenarios - verify each tenant only sees their own data
- IMPORTANT: Test both authenticated and unauthenticated scenarios using helper methods
- IMPORTANT: Verify proper HTTP status codes for missing/invalid tenant headers (Unauthorized for invalid format, NotFound for valid but non-existent tenant)
- IMPORTANT: When creating new multi-tenant features, add comprehensive tests covering all tenant scenarios
