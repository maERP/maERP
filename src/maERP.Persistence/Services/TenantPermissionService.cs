using System;
using System.Threading;
using System.Threading.Tasks;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Services;

public class TenantPermissionService : ITenantPermissionService
{
    private readonly IUserTenantRepository _userTenantRepository;

    public TenantPermissionService(IUserTenantRepository userTenantRepository)
    {
        _userTenantRepository = userTenantRepository;
    }

    public async Task<bool> CanManageUsersAsync(string userId, Guid tenantId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId) || tenantId == Guid.Empty)
        {
            return false;
        }

        return await _userTenantRepository.Entities
            .AsNoTracking()
            .AnyAsync(ut =>
                ut.UserId == userId &&
                ut.TenantId == tenantId &&
                ut.RoleManageUser,
                cancellationToken);
    }

    public async Task<bool> CanManageTenantAsync(string userId, Guid tenantId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId) || tenantId == Guid.Empty)
        {
            return false;
        }

        return await _userTenantRepository.Entities
            .AsNoTracking()
            .AnyAsync(ut =>
                ut.UserId == userId &&
                ut.TenantId == tenantId &&
                ut.RoleManageTenant,
                cancellationToken);
    }
}
