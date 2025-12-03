using System.ComponentModel.DataAnnotations;
using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Tenant.Commands.TenantUserAdd;

/// <summary>
/// Command to add a user to a tenant by email address.
/// </summary>
public class TenantUserAddCommand : IRequest<Result<bool>>
{
    /// <summary>
    /// The ID of the tenant to add the user to.
    /// </summary>
    [Required]
    public Guid TenantId { get; set; }

    /// <summary>
    /// The ID of the current user (for permission checks).
    /// </summary>
    [Required]
    public string CurrentUserId { get; set; } = string.Empty;

    /// <summary>
    /// The email address of the user to add.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Whether this should be the user's default tenant.
    /// </summary>
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// Whether the user should have user management permissions.
    /// </summary>
    public bool RoleManageUser { get; set; } = false;
}
