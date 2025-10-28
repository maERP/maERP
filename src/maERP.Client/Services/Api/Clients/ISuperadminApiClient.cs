using maERP.Application.Features.Superadmin.Commands.SuperadminCreate;
using maERP.Application.Features.Superadmin.Commands.SuperadminUpdate;
using maERP.Application.Features.Superadmin.Users.Commands.UserCreate;
using maERP.Application.Features.Superadmin.Users.Commands.UserUpdate;
using maERP.Application.Features.Superadmin.UserTenants.Commands.AssignUserToTenant;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for superadmin operations
/// </summary>
public interface ISuperadminApiClient
{
    // Tenant operations

    /// <summary>
    /// Get paginated list of tenants
    /// </summary>
    Task<PaginatedResult<TenantListDto>?> GetTenantsAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tenant details by ID
    /// </summary>
    Task<TenantDetailDto?> GetTenantByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new tenant
    /// </summary>
    Task<HttpResponseMessage> CreateTenantAsync(SuperadminCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing tenant
    /// </summary>
    Task<HttpResponseMessage> UpdateTenantAsync(Guid id, SuperadminUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a tenant
    /// </summary>
    Task<HttpResponseMessage> DeleteTenantAsync(Guid id, CancellationToken cancellationToken = default);

    // User operations

    /// <summary>
    /// Get paginated list of users
    /// </summary>
    Task<PaginatedResult<UserListDto>?> GetUsersAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user details by ID
    /// </summary>
    Task<UserDetailDto?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new user
    /// </summary>
    Task<HttpResponseMessage> CreateUserAsync(UserCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing user
    /// </summary>
    Task<HttpResponseMessage> UpdateUserAsync(string id, UserUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a user
    /// </summary>
    Task<HttpResponseMessage> DeleteUserAsync(string id, CancellationToken cancellationToken = default);

    // User-Tenant assignment operations

    /// <summary>
    /// Get tenant assignments for a user
    /// </summary>
    Task<Result<List<UserTenantAssignmentDto>>?> GetUserTenantsAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Assign a user to a tenant
    /// </summary>
    Task<HttpResponseMessage> AssignUserToTenantAsync(string userId, AssignUserToTenantCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove a user from a tenant
    /// </summary>
    Task<HttpResponseMessage> RemoveUserFromTenantAsync(string userId, Guid tenantId, CancellationToken cancellationToken = default);
}
