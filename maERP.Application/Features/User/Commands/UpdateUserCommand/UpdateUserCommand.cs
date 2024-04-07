using MediatR;

namespace maERP.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommand : IRequest<int>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}