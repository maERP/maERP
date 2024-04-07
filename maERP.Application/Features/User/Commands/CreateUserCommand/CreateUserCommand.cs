using MediatR;

namespace maERP.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}