using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

/// <summary>
/// Query for retrieving detailed information about a specific product.
/// Implements IRequest to work with MediatR, returning product details wrapped in a Result.
/// </summary>
public class ProductDetailQuery : IRequest<Result<ProductDetailDto>>
{
    /// <summary>
    /// The unique identifier of the product to retrieve
    /// </summary>
    public int Id { get; set; }
}