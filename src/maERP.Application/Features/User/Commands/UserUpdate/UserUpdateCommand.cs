using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using System.Collections.Generic;

namespace maERP.Application.Features.User.Commands.UserUpdate;

/// <summary>
/// Command for updating an existing user in the system.
/// Implements IRequest to work with MediatR, returning the ID of the updated user wrapped in a Result.
/// </summary>
public class UserUpdateCommand : IRequest<Result<string>>
{
    /// <summary>
    /// The unique identifier of the user to update
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Updated email address for the user
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// First name of the user
    /// </summary>
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the user
    /// </summary>
    public string Lastname { get; set; } = string.Empty;

    /// <summary>
    /// Password for the user account (if updating)
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Default tenant ID for the user
    /// </summary>
    public Guid? DefaultTenantId { get; set; }

    /// <summary>
    /// List of tenant IDs this user should be assigned to
    /// </summary>
    public List<Guid> TenantIds { get; set; } = new List<Guid>();
}