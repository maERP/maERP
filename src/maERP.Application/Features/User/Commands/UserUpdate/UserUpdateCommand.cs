using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserUpdate;

public class UserUpdateCommand : IRequest<Result<string>>
{
    public string Id { get; set; } = string.Empty;     
    public string Email { get; set; } = string.Empty;
}