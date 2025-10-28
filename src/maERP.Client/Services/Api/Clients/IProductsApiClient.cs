using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for product operations
/// </summary>
public interface IProductsApiClient
{
    /// <summary>
    /// Get paginated list of products
    /// </summary>
    Task<PaginatedResult<ProductListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get product details by ID
    /// </summary>
    Task<ProductDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new product
    /// </summary>
    Task<ProductDetailDto?> CreateAsync(ProductCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing product
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, ProductUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a product
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
