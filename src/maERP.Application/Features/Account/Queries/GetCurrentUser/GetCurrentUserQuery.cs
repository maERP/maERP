using maERP.Application.Mediator;
using maERP.Domain.Dtos.Account;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Account.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<Result<CurrentUserProfileDto>>
{
}
