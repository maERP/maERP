# CLAUDE.md — maERP.Persistence

EF Core data access layer. Provider-neutral; the four concrete providers live in sibling projects:

- `maERP.Persistence.MSSQL`
- `maERP.Persistence.PostgreSQL`
- `maERP.Persistence.SQLite`

Refer to the root `/CLAUDE.md` for cross-cutting rules.

## Layout

```
DatabaseContext/   ApplicationDbContext et al. — query filters live here
Repositories/      Generic + entity-specific repository implementations
Configurations/    IEntityTypeConfiguration<T> implementations (Fluent API)
  Options/         DatabaseOptions, etc.
Seeders/           Initial data
Services/          Repository-level services
PersistenceServiceRegistration.cs   Provider switch + DI wiring
```

## Multi-Tenancy via Query Filters

- All tenant-scoped entities derive from `BaseEntity` (`Guid? TenantId`). Apply a global query filter in the `DbContext` for each — read `TenantId` from the injected `ITenantContext`.
- Cross-tenant operations (Superadmin) must explicitly call `IgnoreQueryFilters()` — and only inside the dedicated handlers.
- Never re-filter on `TenantId` in handlers; the filter is the contract.

## Entities & Ids

- `BaseEntity` defines `Guid Id`, `DateTime DateCreated`, `DateTime DateModified`, `Guid? TenantId`.
- `BaseEntityWithoutTenant` for non-scoped data (Tenant itself, global Settings, …).
- All Ids are `System.Guid`. Do not introduce `int` keys.
- Repositories should set `DateModified = DateTime.UtcNow` on update.

## Cascade Deletes

Cascade deletion is **not** configured by EF defaults across the schema. Implement deletes explicitly in the handler or repository — fetch dependents, remove them, save. This is intentional to keep tenancy and audit behavior predictable.

## Migrations

Multi-provider — each provider project owns its own migration assembly so EF can target the right SQL dialect.

```bash
# All four providers
./create-migrations.sh "MigrationName"

# One provider
./create-migrations.sh "MigrationName" mssql
./create-migrations.sh "MigrationName" postgresql
./create-migrations.sh "MigrationName" sqlite
```

**Never create migrations without asking the user first** — schema changes are deliberate and reviewed.

## Pagination

Zero-based pagination, implemented in `QueryableExtensions.cs` (in `maERP.Application` or this layer — see the file). New endpoints **must** use it; do not roll your own.

## Provider Selection

`PersistenceServiceRegistration.cs` reads `DatabaseOptions.Provider` and registers the matching EF Core provider with the matching migrations assembly. Add a new provider only by adding a new sibling project — do not branch inside the main persistence project.

## Tests

Persistence-level tests live in `tests/maERP.Persistence.Tests` (xUnit, EF Core InMemory). Integration tests that hit the API surface live in `tests/maERP.Server.Tests` — see its `CLAUDE.md`.
