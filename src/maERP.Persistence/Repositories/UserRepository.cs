using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<List<ApplicationUser>> GetAllAsync()
    {
        return await _userManager.Users.AsNoTracking().ToListAsync();
    }

    public async Task<ApplicationUser?> GetByIdAsync(string userId)
    {
        return await _userManager.Users
            .Where(x => x.Id == userId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<ApplicationUser?> GetByIdWithTenantsAsync(string userId)
    {
        return await _userManager.Users
            .Include(u => u.UserTenants!)
            .ThenInclude(ut => ut.Tenant)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        var normalizedEmail = _userManager.NormalizeEmail(email);
        return await _userManager.Users.AnyAsync(u => u.NormalizedEmail == normalizedEmail);
    }

    public async Task<IEnumerable<IdentityError>> CreateAsync(ApplicationUser userToCreate, string password)
    {
        userToCreate.PasswordHash = _userManager.PasswordHasher.HashPassword(userToCreate, password);

        var result = await _userManager.CreateAsync(userToCreate, password);

        if (result.Succeeded)
        {
            var roleExists = await _dbContext.Roles.AnyAsync(r => r.Name == "User");
            if (roleExists)
            {
                await _userManager.AddToRoleAsync(userToCreate, "User");
            }
        }

        return result.Errors;
    }

    public async Task<ApplicationUser> UpdateWithDetailsAsync(ApplicationUser userUpdateDto)
    {
        var localUser = await _userManager.FindByIdAsync(userUpdateDto.Id);

        if (localUser != null)
        {
            userUpdateDto.Email = userUpdateDto.Email?.ToLower();

            localUser.Email = userUpdateDto.Email;
            localUser.UserName = userUpdateDto.Email;
            localUser.Firstname = userUpdateDto.Firstname;
            localUser.Lastname = userUpdateDto.Lastname;

            /*
            if (userUpdateDto.Password.Length > 0)
            {
                localUser.PasswordHash = _userManager.PasswordHasher.HashPassword(localUser, userUpdateDto.Password);
            }*/

            await _userManager.UpdateAsync(localUser);
        }
        else
        {
            throw new NotFoundException("User not found", userUpdateDto.Id);
        }

        return userUpdateDto;
    }

    public async Task<bool> Exists(string id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }

    public async Task<bool> TenantsExistAsync(IEnumerable<Guid> tenantIds)
    {
        var ids = tenantIds?.Where(id => id != Guid.Empty).Distinct().ToList() ?? new List<Guid>();
        if (ids.Count == 0)
        {
            return true;
        }

        var existingCount = await _dbContext.Tenant
            .CountAsync(t => ids.Contains(t.Id));

        return existingCount == ids.Count;
    }

    public async Task<IdentityResult> DeleteAsync(ApplicationUser userToDelete)
    {
        var assignments = await _dbContext.UserTenant
            .Where(ut => ut.UserId == userToDelete.Id)
            .ToListAsync();

        if (assignments.Count > 0)
        {
            _dbContext.UserTenant.RemoveRange(assignments);
            await _dbContext.SaveChangesAsync();
        }

        var identityResult = await _userManager.DeleteAsync(userToDelete);

        if (identityResult.Succeeded)
        {
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
        }

        return identityResult;
    }

    // New methods for managing user-tenant assignments

    public async Task AssignUserToTenantsAsync(string userId, IEnumerable<Guid> tenantIds, Guid? defaultTenantId = null)
    {
        // Validate the user exists
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException("User not found", userId);

        // Create user-tenant records for each tenant
        foreach (var tenantId in tenantIds)
        {
            // Check if the assignment already exists
            var existingAssignment = await _dbContext.UserTenant
                .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TenantId == tenantId);

            if (existingAssignment == null)
            {
                // Create new assignment
                var userTenant = new UserTenant
                {
                    UserId = userId,
                    TenantId = tenantId,
                    IsDefault = defaultTenantId.HasValue && tenantId == defaultTenantId.Value,
                    RoleManageUser = false,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                await _dbContext.UserTenant.AddAsync(userTenant);
            }
            else if (defaultTenantId.HasValue && tenantId == defaultTenantId.Value && !existingAssignment.IsDefault)
            {
                // Update existing assignment to be default if needed
                existingAssignment.IsDefault = true;
                existingAssignment.DateModified = DateTime.UtcNow;
                _dbContext.UserTenant.Update(existingAssignment);
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserTenantAssignmentsAsync(string userId, IEnumerable<Guid> tenantIds, Guid? defaultTenantId = null)
    {
        // Validate the user exists
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException("User not found", userId);

        // Get current tenant assignments
        var currentAssignments = await _dbContext.UserTenant
            .Where(ut => ut.UserId == userId)
            .ToListAsync();

        // Remove assignments that are not in the new list
        var tenantsToKeep = tenantIds.ToList();
        var assignmentsToRemove = currentAssignments
            .Where(a => !tenantsToKeep.Contains(a.TenantId))
            .ToList();

        foreach (var assignment in assignmentsToRemove)
        {
            _dbContext.UserTenant.Remove(assignment);
        }

        // Add new assignments
        foreach (var tenantId in tenantsToKeep)
        {
            var existingAssignment = currentAssignments
                .FirstOrDefault(a => a.TenantId == tenantId);

            if (existingAssignment == null)
            {
                // Create new assignment
                var userTenant = new UserTenant
                {
                    UserId = userId,
                    TenantId = tenantId,
                    IsDefault = defaultTenantId.HasValue && tenantId == defaultTenantId.Value,
                    RoleManageUser = false,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                await _dbContext.UserTenant.AddAsync(userTenant);
            }
            else if (defaultTenantId.HasValue && tenantId == defaultTenantId.Value && !existingAssignment.IsDefault)
            {
                // Update existing assignment to be default if needed
                existingAssignment.IsDefault = true;
                existingAssignment.DateModified = DateTime.UtcNow;
            }
            else if (defaultTenantId.HasValue && tenantId != defaultTenantId.Value && existingAssignment.IsDefault)
            {
                // Remove default flag if this is no longer the default
                existingAssignment.IsDefault = false;
                existingAssignment.DateModified = DateTime.UtcNow;
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<UserTenant>> GetUserTenantAssignmentsAsync(string userId)
    {
        return await _dbContext.UserTenant
            .Where(ut => ut.UserId == userId)
            .Include(ut => ut.Tenant)
            .ToListAsync();
    }

    public async Task<bool> IsUserAssignedToTenantAsync(string userId, Guid tenantId)
    {
        return await _dbContext.UserTenant
            .AnyAsync(ut => ut.UserId == userId && ut.TenantId == tenantId);
    }
}
