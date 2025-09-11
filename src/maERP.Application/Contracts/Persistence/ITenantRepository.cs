using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ITenantRepository : IGenericRepository<Tenant>
{
    Task<Tenant?> GetByTenantCodeAsync(string tenantCode);
    Task<bool> TenantCodeExistsAsync(string tenantCode);
    Task<bool> TenantCodeExistsAsync(string tenantCode, Guid excludeId);
    Task<IEnumerable<Tenant>> GetActivTenantsAsync();
}