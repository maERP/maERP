# CLAUDE.md — maERP.Server.Tests

xUnit integration tests against `maERP.Server`, with first-class multi-tenant support.

Refer to the root `/CLAUDE.md` for cross-cutting test rules. This file covers the multi-tenant test infrastructure that all Server integration tests must use.

## Hard Rules

- **All multi-tenant tests MUST inherit `TenantIsolatedTestBase`.** Do not roll your own `WebApplicationFactory` or DbContext setup.
- **Don't use `FluentAssertions`.** Use plain xUnit `Assert.*` or the `TestAssertions` helper.
- **Per-test factories**, never shared fixtures — each test gets an isolated InMemory database.
- When a test fails, first check the test logic. Only fix production code if the test is correct.

## TenantIsolatedTestBase

Location: `tests/maERP.Server.Tests/Infrastructure/TenantIsolatedTestBase.cs`.

```csharp
public class MyTenantAwareTests : TenantIsolatedTestBase
{
    [Fact]
    public async Task GetCustomers_WithTenant1_ReturnsTenant1DataOnly()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        // assert only Tenant1 data is present
    }
}
```

Provided by the base class:
- `Client` — `HttpClient` wired to a test server with InMemory DB
- `DbContext` — direct EF Core access for arrangement/assertion
- `TenantContext` — in-memory `ITenantContext`

### Helper methods

| Helper | Purpose |
|---|---|
| `SetTenantHeader(Guid)` | Set valid `X-Tenant-Id` header |
| `SetInvalidTenantHeader()` | Set a syntactically valid GUID that doesn't exist |
| `SetInvalidTenantHeaderValue(string)` | Set a malformed (non-GUID) header value |
| `RemoveTenantHeader()` | Drop the header entirely |
| `SimulateAuthenticatedRequest()` / `SimulateUnauthenticatedRequest()` | Toggle auth |
| `PostAsJsonAsync<T>()`, `PutAsJsonAsync<T>()` | HTTP helpers |
| `ReadResponseAsync<T>()`, `ReadResponseStringAsync()` | Response parsing |

## TestDataSeeder

Location: `tests/maERP.Server.Tests/Infrastructure/TestDataSeeder.cs`. Idempotent. Seeds:
- 3 Tenants (`TestTenant1Id`, `TestTenant2Id`, `TestTenant3Id` from `TenantConstants`)
- 4 TaxClasses, 4 Warehouses, 4 SalesChannels, 3 AiModels, 3 AiPrompts
- SalesChannel↔Warehouse mappings

Call once per test:
```csharp
await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
```

## TestAssertions

Location: `tests/maERP.Server.Tests/Infrastructure/TestAssertions.cs`. Thin wrappers around xUnit for common needs (HTTP status, nullability, collections).

## Required Coverage for New Multi-Tenant Features

Every tenant-scoped feature **must** include tests for:

1. **Isolation** — Tenant 1 sees only Tenant 1 data; Tenant 2 sees only its own.
2. **Cross-tenant access prevention** — Tenant 1 cannot read/update/delete a Tenant 2 record (expect `404`/`403`, never the data).
3. **Missing tenant header** — request without `X-Tenant-Id` is rejected.
4. **Invalid tenant header**:
   - Malformed (non-GUID) → `401 Unauthorized`
   - Valid GUID but non-existent → `404 NotFound`
5. **Authentication** — both authenticated and unauthenticated paths.

`TestAuthenticationHandler` (`Infrastructure/`) implements a header-driven auth scheme so tests can spoof user identities and roles.

## Other Infrastructure Files

| File | Role |
|---|---|
| `TestWebApplicationFactory<TProgram>.cs` | Configures DI, InMemory DB, test auth |
| `TestAuthenticationHandler.cs` | Header-based fake authentication |
| `TestTenantContext.cs` | In-memory `ITenantContext` for tests |
| `CurrentUserHelper.cs` | Default test user IDs |
| `GlobalTestBase.cs` | Lower-level shared base |
| `TenantConstants.cs` | The three test tenant GUIDs |

## Conventions

- Test classes named `{Feature}{Action}Tests` (e.g. `CustomerCreateTest`, `OrderListTests`).
- One assertion concept per test — split scenarios rather than packing them.
- Arrange / Act / Assert with blank lines between phases.
- Use `TenantConstants.TestTenant1Id` etc., never hard-coded GUIDs.
