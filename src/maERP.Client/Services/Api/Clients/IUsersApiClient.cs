using maERP.Application.Features.Superadmin.Users.Commands.UserCreate;
using maERP.Application.Features.Superadmin.Users.Commands.UserUpdate;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for user operations
/// </summary>
public interface IUsersApiClient
{
    /// <summary>
    /// Get paginated list of users
    /// </summary>
    Task<PaginatedResult<UserListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user details by ID
    /// </summary>
    Task<UserDetailDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new user
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(UserCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing user
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(string id, UserUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a user
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
