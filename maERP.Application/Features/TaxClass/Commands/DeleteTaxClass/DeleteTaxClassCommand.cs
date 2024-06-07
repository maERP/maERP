using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.DeleteTaxClass;

public class DeleteTaxClassCommand : IRequest<int>
{
    public int Id { get; set; }     
}