using MediatR;

namespace maERP.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommand : IRequest<string>
{
    public int Id { get; set; }     
    public string Email { get; set; } = string.Empty;
}