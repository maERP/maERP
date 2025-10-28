namespace maERP.Client.Services.Tenant;

/// <summary>
/// Service for managing the current tenant context in multi-tenant scenarios
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Gets the current tenant ID
    /// </summary>
    Guid? GetCurrentTenantId();

    /// <summary>
    /// Sets the current tenant ID
    /// </summary>
    void SetCurrentTenantId(Guid tenantId);

    /// <summary>
    /// Clears the current tenant context
    /// </summary>
    void ClearTenantContext();

    /// <summary>
    /// Checks if a tenant context is set
    /// </summary>
    bool HasTenantContext();

    /// <summary>
    /// Event raised when tenant context changes
    /// </summary>
    event EventHandler<Guid?>? TenantChanged;
}
