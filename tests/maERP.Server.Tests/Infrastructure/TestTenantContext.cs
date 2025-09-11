using maERP.Application.Contracts.Services;
using System.Collections.Concurrent;

namespace maERP.Server.Tests.Infrastructure;

public class TestTenantContext : ITenantContext
{
    // Use instance variables for proper per-request isolation in tests
    private Guid? _currentTenantId;
    private HashSet<Guid> _assignedTenantIds = new HashSet<Guid>();

    public Guid? GetCurrentTenantId()
    {
        return _currentTenantId;
    }

    public void SetCurrentTenantId(Guid? tenantId)
    {
        // In test mode, allow setting any tenant ID to facilitate testing
        _currentTenantId = tenantId;
    }

    public bool HasTenant()
    {
        return _currentTenantId.HasValue;
    }

    public IReadOnlyCollection<Guid> GetAssignedTenantIds()
    {
        return _assignedTenantIds;
    }

    public void SetAssignedTenantIds(IEnumerable<Guid> tenantIds)
    {
        _assignedTenantIds = new HashSet<Guid>(tenantIds ?? new List<Guid>());
    }

    public bool IsAssignedToTenant(Guid tenantId)
    {
        return _assignedTenantIds.Contains(tenantId);
    }
}