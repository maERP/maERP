using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

public class ProductDetailQuery : IRequest<Result<ProductDetailDto>>
{
    public int Id { get; set; }
}