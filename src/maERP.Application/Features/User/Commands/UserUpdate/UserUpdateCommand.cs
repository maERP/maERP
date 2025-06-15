using maERP.Domain.Wrapper;
using MediatR;

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
}