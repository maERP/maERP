using MediatR;

namespace maERP.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<int>
{
    public int Id { get; set; }     
}