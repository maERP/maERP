using maERP.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> GetByIdAsync(string userId);
    Task<ApplicationUser?> GetByIdWithTenantsAsync(string userId);
    Task<bool> EmailExistsAsync(string email);
    Task<IEnumerable<IdentityError>> CreateAsync(ApplicationUser userToCreate, string password);
    Task<ApplicationUser> UpdateWithDetailsAsync(ApplicationUser userUpdateDto);
    Task<bool> Exists(string id);
    Task<bool> TenantsExistAsync(IEnumerable<Guid> tenantIds);
    Task<IdentityResult> DeleteAsync(ApplicationUser userToDelete);

    // New methods for managing user-tenant assignments
    Task AssignUserToTenantsAsync(string userId, IEnumerable<Guid> tenantIds, Guid? defaultTenantId = null);
    Task UpdateUserTenantAssignmentsAsync(string userId, IEnumerable<Guid> tenantIds, Guid? defaultTenantId = null);
    Task<List<UserTenant>> GetUserTenantAssignmentsAsync(string userId);
    Task<bool> IsUserAssignedToTenantAsync(string userId, Guid tenantId);
}
