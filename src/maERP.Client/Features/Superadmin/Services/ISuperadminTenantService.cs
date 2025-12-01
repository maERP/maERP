using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Tenant;

namespace maERP.Client.Features.Superadmin.Services;

/// <summary>
/// Service interface for superadmin tenant-related API operations.
/// Uses the /api/v1/superadmin/tenants endpoint.
/// </summary>
public interface ISuperadminTenantService
{
    /// <summary>
    /// Gets a paginated list of all tenants (superadmin only).
    /// </summary>
    Task<PaginatedResponse<TenantListDto>> GetTenantsAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single tenant by ID (superadmin only).
    /// </summary>
    Task<TenantDetailDto?> GetTenantAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing tenant (superadmin only).
    /// </summary>
    Task UpdateTenantAsync(Guid id, TenantInputDto input, CancellationToken ct = default);
}
