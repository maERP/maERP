using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

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
}