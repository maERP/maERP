using maERP.Domain.Dtos.Product;
using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

public class ProductDetailQuery : IRequest<ProductDetailDto>
{
    public int Id { get; set; }
}