using maERP.Domain.Dtos.Tenant;

namespace maERP.Application.Contracts.Identity;

public interface IUserTenantService
{
    Task<List<TenantListDto>> GetUserTenantsAsync(string userId);
}