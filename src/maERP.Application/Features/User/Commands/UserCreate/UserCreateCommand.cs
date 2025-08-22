using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using System.Collections.Generic;

namespace maERP.Application.Features.User.Commands.UserCreate;

/// <summary>
/// Command for creating a new user in the system.
/// Implements IRequest to work with MediatR, returning the ID of the newly created user wrapped in a Result.
/// </summary>
public class UserCreateCommand : IRequest<Result<string>>
{
    /// <summary>
    /// Email address of the user to create
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Password for the new user account
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// First name of the user
    /// </summary>
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the user
    /// </summary>
    public string Lastname { get; set; } = string.Empty;

    /// <summary>
    /// Default tenant ID for the user
    /// </summary>
    public int DefaultTenantId { get; set; }

    /// <summary>
    /// Additional tenant IDs to assign to this user
    /// </summary>
    public List<int> AdditionalTenantIds { get; set; } = new List<int>();
}