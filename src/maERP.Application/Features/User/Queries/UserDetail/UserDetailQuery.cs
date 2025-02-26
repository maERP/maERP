using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Queries.UserDetail;

public class UserDetailQuery : IRequest<Result<UserDetailDto>>
{
    public string Id { get; set; } = string.Empty;
}