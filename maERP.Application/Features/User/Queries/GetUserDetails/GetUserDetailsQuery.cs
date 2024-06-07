using MediatR;

namespace maERP.Application.Features.User.Queries.GetUserDetails;

public class GetUserDetailsQuery : IRequest<UserDetailDto>
{
    public string Id { get; set; } = string.Empty;
}