using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ITenantRepository : IGenericRepository<Tenant>
{
    Task DeleteTenantWithCascadeAsync(Guid tenantId, CancellationToken cancellationToken = default);
}