using MediatR;

namespace maERP.Application.Features.User.Queries.GetUserDetails;

public class GetUserDetailsQuery : IRequest<GetUserDetailResponse>
{
    public string Id { get; set; } = string.Empty;
}