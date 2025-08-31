using maERP.Application.Contracts.Services;
using System.Collections.Concurrent;

namespace maERP.Server.Tests.Infrastructure;

public class TestTenantContext : ITenantContext
{
    // For tests, use a simple static variable - this is acceptable since tests run in isolation
    private static int? _globalCurrentTenantId = null;
    private readonly HashSet<int> _assignedTenantIds = new HashSet<int>();

    public int? GetCurrentTenantId()
    {
        return _globalCurrentTenantId;
    }

    public void SetCurrentTenantId(int? tenantId)
    {
        _globalCurrentTenantId = tenantId;
    }

    public bool HasTenant()
    {
        var currentTenantId = GetCurrentTenantId();
        return currentTenantId.HasValue;
    }

    public IReadOnlyCollection<int> GetAssignedTenantIds()
    {
        return _assignedTenantIds;
    }

    public void SetAssignedTenantIds(IEnumerable<int> tenantIds)
    {
        _assignedTenantIds.Clear();
        foreach (var tenantId in tenantIds ?? new List<int>())
        {
            _assignedTenantIds.Add(tenantId);
        }
    }

    public bool IsAssignedToTenant(int tenantId)
    {
        return _assignedTenantIds.Contains(tenantId);
    }
}