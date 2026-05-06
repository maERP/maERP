# CLAUDE.md — maERP.Server

ASP.NET Core Web API (net10.0). Headless backend; consumed by `maERP.Client` and any HTTP client.

Refer to the root `/CLAUDE.md` for cross-cutting rules. This file covers Server-specific conventions.

## Layout

```
Controllers/Api/V1/        REST endpoints (versioned)
Filters/                   Authorization & action filters, GlobalExceptionFilter
Middleware/                Request pipeline (tenant resolution, etc.)
ServiceRegistrations/      DI wiring per concern
Infrastructure/JsonConverters/   StrictEnumConverter, etc.
Extensions/                ToActionResult() and similar helpers
Views/                     Razor views for tracking/web pages
```

## Controller Pattern

Every controller follows this shape — copy an existing one (`CustomersController.cs`) when adding a new resource.

```csharp
[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class CustomersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<CustomerListDto>>> GetAll(
        int pageNumber = 0, int pageSize = 10, string searchString = "", string salesBy = "")
    {
        var response = await mediator.Send(
            new CustomerListQuery(pageNumber, pageSize, searchString, salesBy));
        return response.ToActionResult();
    }
}
```

Rules:
- **Primary-constructor injection** of `IMediator` (the custom one from `maERP.Application.Mediator`).
- Controllers are thin: build the request, send via mediator, return `response.ToActionResult()`.
- Always declare `[ProducesResponseType]` for documented status codes (Swagger).
- Pagination is zero-based — defaults `pageNumber = 0, pageSize = 10`.
- Route ids use `{id:guid}` constraint.

## CQRS in maERP.Application

Commands and queries live in `src/maERP.Application/Features/{Area}/{Commands|Queries}/{Name}/`. Each folder typically contains:

- `{Name}Command.cs` or `{Name}Query.cs` — the request, implements `IRequest<TResponse>`
- `{Name}Handler.cs` — implements `IRequestHandler<TRequest, TResponse>`
- `{Name}Validator.cs` — FluentValidation rules (where input is non-trivial)

The Mediator is **custom** — `maERP.Application.Mediator.CustomMediator` resolves `IRequestHandler<,>` via DI and dispatches by reflection. Do not add the `MediatR` NuGet package; some legacy doc comments mention it but the implementation is in-house.

Returns are wrapped in `Result<T>` / `PaginatedResult<T>` (`maERP.Domain.Wrapper`). The extension `response.ToActionResult()` maps `Result` to the right HTTP status with RFC 7807 problem details on failures.

## Roles & Authorization

See root `CLAUDE.md` for the role matrix. In Server code:

- `[Authorize]` at controller level by default; per-action `[Authorize(Roles = "...")]` for elevated operations.
- `SuperadminController` is the only place where cross-tenant user/tenant operations are permitted.
- Tenant resolution comes from middleware, exposed via `ITenantContext`. Handlers should rely on the EF Core query filter — they should not fetch `TenantId` and re-filter manually.

## Validation & Error Handling

- **FluentValidation** registered via `FluentValidation.DependencyInjectionExtensions`.
- A global exception filter converts unhandled exceptions to RFC 7807 `ProblemDetails`.
- Don't catch and swallow — let the global filter handle it unless you have a specific recovery path.

## Versioning & Swagger

- API version via `Asp.Versioning.Mvc.ApiExplorer` (currently `v1.0`). New endpoints stay on `v1.0` unless the contract is breaking.
- Swashbuckle is wired up; Swagger UI exposes the spec in development.

## Common Tasks

| Task | Where |
|---|---|
| Add a CRUD resource | New `Features/{Area}/...` folder + `{Area}Controller.cs` + DTOs in `maERP.Domain/Dtos/{Area}` |
| New auth role | `maERP.Identity` constants + role check in handler + tests |
| Cross-tenant operation | Goes in `SuperadminController` only |
| Cascade delete | Implement explicitly in the handler/repository — do not rely on EF defaults |

## Tests

Server integration tests live in `tests/maERP.Server.Tests` (xUnit). All multi-tenant tests inherit `TenantIsolatedTestBase`. See that project's `CLAUDE.md`.
