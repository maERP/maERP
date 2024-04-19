using maERP.Application.Dtos.Product;
using MediatR;

namespace maERP.Application.Features.Product.Queries.GetProductDetailQuery;

public class GetProductDetailQuery : IRequest<ProductDetailDto>
{
    public int Id { get; set; }
}