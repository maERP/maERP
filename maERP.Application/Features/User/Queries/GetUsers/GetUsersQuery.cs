using MediatR;

namespace maERP.Application.Features.User.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<GetUsersResponse>>;