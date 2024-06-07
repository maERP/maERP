using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.UpdateTaxClass;

public class UpdateTaxClassCommand : IRequest<int>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}