using MediatR;

namespace maERP.Application.Features.User.Queries.UserList;

public record UserListQuery : IRequest<List<UserListResponse>>;