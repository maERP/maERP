using MediatR;

namespace maERP.Application.Features.User.Commands.UserUpdate;

public class UserUpdateCommand : IRequest<string>
{
    public int Id { get; set; }     
    public string Email { get; set; } = string.Empty;
}