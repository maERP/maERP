using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.User.Queries.UserDetail;

/// <summary>
/// Query for retrieving detailed information about a specific user.
/// Implements IRequest to work with MediatR, returning user details wrapped in a Result.
/// </summary>
public class UserDetailQuery : IRequest<Result<UserDetailDto>>
{
    /// <summary>
    /// The unique identifier of the user to retrieve
    /// </summary>
    public string Id { get; set; } = string.Empty;
}