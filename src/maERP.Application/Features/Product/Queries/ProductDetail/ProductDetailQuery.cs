using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

public class ProductDetailQuery : IRequest<ProductDetailResponse>
{
    public int Id { get; set; }
}