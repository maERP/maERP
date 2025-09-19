using System;
using System.Threading;
using System.Threading.Tasks;

namespace maERP.Application.Contracts.Services;

public interface ITenantPermissionService
{
    Task<bool> CanManageUsersAsync(string userId, Guid tenantId, CancellationToken cancellationToken = default);
}
