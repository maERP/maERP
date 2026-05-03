using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Contracts.Persistence;

/// <summary>
/// Repository for the <c>TenantOAuthAppSettings</c> sibling of <c>TenantEmailSettings</c>.
/// Does not inherit <c>IGenericRepository&lt;T&gt;</c> because the entity is
/// <c>BaseEntityWithoutTenant</c>; the small per-method surface here is enough.
/// </summary>
public interface ITenantOAuthAppSettingsRepository
{
    Task<List<TenantOAuthAppSettings>> GetByTenantIdAsync(Guid tenantId);

    Task<TenantOAuthAppSettings?> GetByTenantAndProviderAsync(Guid tenantId, SalesChannelType provider);

    Task<Guid> CreateAsync(TenantOAuthAppSettings entity);

    Task UpdateAsync(TenantOAuthAppSettings entity);

    Task DeleteAsync(TenantOAuthAppSettings entity);
}
