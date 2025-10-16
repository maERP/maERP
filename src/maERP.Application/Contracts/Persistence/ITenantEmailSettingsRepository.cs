using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ITenantEmailSettingsRepository : IGenericRepository<TenantEmailSettings>
{
    /// <summary>
    /// Gets the active email settings for a specific tenant
    /// </summary>
    Task<TenantEmailSettings?> GetByTenantIdAsync(Guid tenantId);

    /// <summary>
    /// Gets the active email settings for a specific tenant, or null if none exists
    /// </summary>
    Task<TenantEmailSettings?> GetActiveTenantSettingsAsync(Guid tenantId);
}
