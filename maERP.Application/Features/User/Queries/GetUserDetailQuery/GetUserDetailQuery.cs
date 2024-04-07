using maERP.Application.Dtos.User;
using MediatR;

namespace maERP.Application.Features.User.Queries.GetUserDetailQuery;

public class GetUserDetailQuery : IRequest<UserDetailDto>
{
    public string Id { get; set; }
}