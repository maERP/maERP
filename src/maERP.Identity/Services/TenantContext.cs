using maERP.Application.Contracts.Services;
using System.Collections.Generic;

namespace maERP.Identity.Services;

public class TenantContext : ITenantContext
{
    private int? _currentTenantId;
    private HashSet<int> _assignedTenantIds = new HashSet<int>();

    public int? GetCurrentTenantId()
    {
        return _currentTenantId;
    }

    public void SetCurrentTenantId(int? tenantId)
    {
        // Only allow setting to null or a tenant that the user is assigned to
        if (tenantId == null || (_assignedTenantIds.Count > 0 && _assignedTenantIds.Contains(tenantId.Value)))
        {
            _currentTenantId = tenantId;
        }
    }

    public bool HasTenant()
    {
        return _currentTenantId.HasValue;
    }
    
    /// <summary>
    /// Gets all tenant IDs assigned to the current user
    /// </summary>
    /// <returns>Collection of tenant IDs the user can access</returns>
    public IReadOnlyCollection<int> GetAssignedTenantIds()
    {
        return _assignedTenantIds;
    }
    
    /// <summary>
    /// Sets the collection of tenant IDs that the current user can access
    /// </summary>
    /// <param name="tenantIds">Collection of tenant IDs</param>
    public void SetAssignedTenantIds(IEnumerable<int> tenantIds)
    {
        _assignedTenantIds = new HashSet<int>(tenantIds ?? new List<int>());
        
        // If current tenant is not in the assigned list, reset it
        if (_currentTenantId.HasValue && !_assignedTenantIds.Contains(_currentTenantId.Value))
        {
            _currentTenantId = _assignedTenantIds.Count > 0 ? _assignedTenantIds.First() : null;
        }
    }
    
    /// <summary>
    /// Checks if the user is assigned to a specific tenant
    /// </summary>
    /// <param name="tenantId">Tenant ID to check</param>
    /// <returns>True if the user is assigned to the tenant</returns>
    public bool IsAssignedToTenant(int tenantId)
    {
        return _assignedTenantIds.Contains(tenantId);
    }
}