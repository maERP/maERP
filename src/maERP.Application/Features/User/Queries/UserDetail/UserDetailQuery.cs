using MediatR;

namespace maERP.Application.Features.User.Queries.UserDetail;

public class UserDetailQuery : IRequest<UserDetailResponse>
{
    public string Id { get; set; } = string.Empty;
}