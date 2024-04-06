using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.CreateTaxClassCommand;

public class CreateTaxClassCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}