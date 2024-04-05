using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.DeleteTaxClassCommand;

public class DeleteTaxClassCommand : IRequest<int>
{
    public int Id { get; set; }     
}