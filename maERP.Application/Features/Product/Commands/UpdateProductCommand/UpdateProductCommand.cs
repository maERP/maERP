using MediatR;

namespace maERP.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommand : IRequest<int>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}