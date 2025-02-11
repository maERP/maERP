using MediatR;

namespace maERP.Application.Features.User.Commands.UserDelete;

public class UserDeleteCommand : IRequest<string>
{
    public string Id { get; set; } = string.Empty;
}