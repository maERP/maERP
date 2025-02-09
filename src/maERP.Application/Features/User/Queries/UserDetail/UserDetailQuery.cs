using maERP.Domain.Dtos.User;
using MediatR;

namespace maERP.Application.Features.User.Queries.UserDetail;

public class UserDetailQuery : IRequest<UserDetailDto>
{
    public string Id { get; set; } = string.Empty;
}