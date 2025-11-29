using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Product;

namespace maERP.Client.Features.Products.Services;

/// <summary>
/// Service interface for product-related API operations.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Gets a paginated list of products with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<ProductListDto>> GetProductsAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single product by ID.
    /// </summary>
    Task<ProductDetailDto?> GetProductAsync(Guid id, CancellationToken ct = default);
}
