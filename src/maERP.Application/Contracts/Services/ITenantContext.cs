using System.Collections.Generic;

namespace maERP.Application.Contracts.Services;

public interface ITenantContext
{
    int? GetCurrentTenantId();
    void SetCurrentTenantId(int? tenantId);
    bool HasTenant();

    /// <summary>
    /// Gets all tenant IDs assigned to the current user
    /// </summary>
    /// <returns>Collection of tenant IDs the user can access</returns>
    IReadOnlyCollection<int> GetAssignedTenantIds();

    /// <summary>
    /// Sets the collection of tenant IDs that the current user can access
    /// </summary>
    /// <param name="tenantIds">Collection of tenant IDs</param>
    void SetAssignedTenantIds(IEnumerable<int> tenantIds);

    /// <summary>
    /// Checks if the user is assigned to a specific tenant
    /// </summary>
    /// <param name="tenantId">Tenant ID to check</param>
    /// <returns>True if the user is assigned to the tenant</returns>
    bool IsAssignedToTenant(int tenantId);
}