using MediatR;

namespace maERP.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommand : IRequest<int>
{
    public int Id { get; set; }     
}