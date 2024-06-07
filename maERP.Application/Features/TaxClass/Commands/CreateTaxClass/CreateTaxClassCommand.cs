using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.CreateTaxClass;

public class CreateTaxClassCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}