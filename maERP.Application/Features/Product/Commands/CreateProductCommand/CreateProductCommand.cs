using MediatR;

namespace maERP.Application.Features.Product.Commands.CreateProductCommand;

public class CreateProductCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}