using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Tenant;

namespace maERP.Client.Features.Tenants.Services;

/// <summary>
/// Service interface for tenant-related API operations.
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Gets a paginated list of tenants for the current user with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<TenantListDto>> GetTenantsAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single tenant by ID.
    /// </summary>
    Task<TenantDetailDto?> GetTenantAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new tenant.
    /// </summary>
    Task<Guid> CreateTenantAsync(TenantInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing tenant.
    /// </summary>
    Task UpdateTenantAsync(Guid id, TenantInputDto input, CancellationToken ct = default);
}
