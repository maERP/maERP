using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IUserTenantRepository : IGenericRepository<UserTenant>
{
    Task<List<TenantListDto>> GetUserTenantsAsync(string userId);
}