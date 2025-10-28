using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for tenant operations
/// </summary>
public interface ITenantsApiClient
{
    /// <summary>
    /// Get all tenants for the current user
    /// </summary>
    Task<PaginatedResult<TenantListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tenant details by ID
    /// </summary>
    Task<TenantDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new tenant
    /// </summary>
    Task<TenantDetailDto?> CreateAsync(TenantInputDto input, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing tenant
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, TenantInputDto input, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a tenant
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
