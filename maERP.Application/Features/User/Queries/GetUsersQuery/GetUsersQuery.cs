using maERP.Application.Dtos.User;
using MediatR;

namespace maERP.Application.Features.User.Queries.GetUsersQuery;

public record GetUsersQuery : IRequest<List<UserListDto>>;