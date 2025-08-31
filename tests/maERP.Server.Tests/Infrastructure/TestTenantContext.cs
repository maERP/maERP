using maERP.Application.Contracts.Services;
using System.Collections.Concurrent;

namespace maERP.Server.Tests.Infrastructure;

public class TestTenantContext : ITenantContext
{
    // Use instance variables for proper per-request isolation in tests
    private int? _currentTenantId;
    private HashSet<int> _assignedTenantIds = new HashSet<int>();

    public int? GetCurrentTenantId()
    {
        return _currentTenantId;
    }

    public void SetCurrentTenantId(int? tenantId)
    {
        // In test mode, allow setting any tenant ID to facilitate testing
        _currentTenantId = tenantId;
    }

    public bool HasTenant()
    {
        return _currentTenantId.HasValue;
    }

    public IReadOnlyCollection<int> GetAssignedTenantIds()
    {
        return _assignedTenantIds;
    }

    public void SetAssignedTenantIds(IEnumerable<int> tenantIds)
    {
        _assignedTenantIds = new HashSet<int>(tenantIds ?? new List<int>());
    }

    public bool IsAssignedToTenant(int tenantId)
    {
        return _assignedTenantIds.Contains(tenantId);
    }
}