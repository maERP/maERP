using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}