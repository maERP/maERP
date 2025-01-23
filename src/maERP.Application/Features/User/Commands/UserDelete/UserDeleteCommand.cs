using MediatR;

namespace maERP.Application.Features.User.Commands.UserDelete;

public record UserDeleteCommand(string Id) : IRequest<Unit>;
