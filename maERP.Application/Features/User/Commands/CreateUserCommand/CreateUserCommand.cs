using MediatR;

namespace maERP.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommand : IRequest<string>
{
    public string Email { get; set; } =  string.Empty;  
    public string Password { get; set; } =  string.Empty;
}