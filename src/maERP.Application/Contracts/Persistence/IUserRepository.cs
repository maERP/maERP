using maERP.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> GetByIdAsync(string userId);
    Task<IEnumerable<IdentityError>> CreateAsync(ApplicationUser userToCreate, string password);
    Task<ApplicationUser> UpdateWithDetailsAsync(ApplicationUser userUpdateDto);
    Task<bool> Exists(string id);

    // New methods for managing user-tenant assignments
    Task AssignUserToTenantsAsync(string userId, IEnumerable<int> tenantIds, int? defaultTenantId = null);
    Task UpdateUserTenantAssignmentsAsync(string userId, IEnumerable<int> tenantIds, int? defaultTenantId = null);
    Task<List<UserTenant>> GetUserTenantAssignmentsAsync(string userId);
    Task<bool> IsUserAssignedToTenantAsync(string userId, int tenantId);
}