using maERP.Application.Mediator;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Tenant.Queries.TenantUserSearch;

/// <summary>
/// Query to search for a user by email address.
/// Used when adding users to a tenant.
/// </summary>
public class TenantUserSearchQuery : IRequest<Result<UserListDto?>>
{
    /// <summary>
    /// The ID of the tenant context (for permission checks).
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// The ID of the current user (for permission checks).
    /// </summary>
    public string CurrentUserId { get; set; } = string.Empty;

    /// <summary>
    /// The email address to search for.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
