using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.User.Commands.UserDelete;

/// <summary>
/// Command for deleting an existing user from the system.
/// Implements IRequest to work with MediatR, returning the ID of the deleted user wrapped in a Result.
/// </summary>
public class UserDeleteCommand : IRequest<Result<string>>
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public string Id { get; set; } = string.Empty;
}