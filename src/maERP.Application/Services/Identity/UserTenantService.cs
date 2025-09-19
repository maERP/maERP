using maERP.Application.Contracts.Identity;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Tenant;

namespace maERP.Application.Services.Identity;

public class UserTenantService : IUserTenantService
{
    private readonly IUserTenantRepository _userTenantRepository;

    public UserTenantService(IUserTenantRepository userTenantRepository)
    {
        _userTenantRepository = userTenantRepository;
    }

    public async Task<List<TenantListDto>> GetUserTenantsAsync(string userId)
    {
        return await _userTenantRepository.GetUserTenantsAsync(userId);
    }
}