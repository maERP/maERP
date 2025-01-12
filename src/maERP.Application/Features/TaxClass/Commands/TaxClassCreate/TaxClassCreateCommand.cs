using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

public class TaxClassCreateCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}