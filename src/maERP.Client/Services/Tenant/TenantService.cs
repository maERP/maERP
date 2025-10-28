namespace maERP.Client.Services.Tenant;

/// <summary>
/// Implementation of tenant service for managing multi-tenant context
/// </summary>
public class TenantService : ITenantService
{
    private readonly ILogger<TenantService> _logger;
    private Guid? _currentTenantId;

    public event EventHandler<Guid?>? TenantChanged;

    public TenantService(ILogger<TenantService> logger)
    {
        _logger = logger;
    }

    public Guid? GetCurrentTenantId()
    {
        return _currentTenantId;
    }

    public void SetCurrentTenantId(Guid tenantId)
    {
        if (_currentTenantId != tenantId)
        {
            _currentTenantId = tenantId;
            _logger.LogInformation("Tenant context changed to: {TenantId}", tenantId);
            TenantChanged?.Invoke(this, tenantId);
        }
    }

    public void ClearTenantContext()
    {
        if (_currentTenantId.HasValue)
        {
            _currentTenantId = null;
            _logger.LogInformation("Tenant context cleared");
            TenantChanged?.Invoke(this, null);
        }
    }

    public bool HasTenantContext()
    {
        return _currentTenantId.HasValue;
    }
}
