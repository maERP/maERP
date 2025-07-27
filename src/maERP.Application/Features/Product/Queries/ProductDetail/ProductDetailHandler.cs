using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

/// <summary>
/// Handler for processing product detail queries.
/// Implements IRequestHandler from MediatR to handle ProductDetailQuery requests
/// and return detailed product information wrapped in a Result.
/// </summary>
public class ProductDetailHandler : IRequestHandler<ProductDetailQuery, Result<ProductDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<ProductDetailHandler> _logger;

    /// <summary>
    /// Repository for product data operations
    /// </summary>
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="productRepository">Repository for product data access</param>
    public ProductDetailHandler(
        IAppLogger<ProductDetailHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Handles the product detail query request
    /// </summary>
    /// <param name="request">The query containing the product ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed product information if successful</returns>
    public async Task<Result<ProductDetailDto>> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving product details for ID: {Id}", request.Id);

        var result = new Result<ProductDetailDto>();

        try
        {
            // Retrieve product with all related details from the repository
            var product = await _productRepository.GetByIdAsync(request.Id, true);

            // If product not found, return a not found result
            if (product == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Product with ID {request.Id} not found");

                _logger.LogWarning("Product with ID {Id} not found", request.Id);
                return result;
            }

            // Manual mapping instead of using AutoMapper
            var data = new ProductDetailDto
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                NameOptimized = product.NameOptimized,
                Ean = product.Ean,
                Asin = product.Asin,
                Description = product.Description,
                DescriptionOptimized = product.DescriptionOptimized,
                UseOptimized = product.UseOptimized,
                Price = product.Price,
                Msrp = product.Msrp,
                Weight = product.Weight,
                Width = product.Width,
                Height = product.Height,
                Depth = product.Depth,
                TaxClassId = product.TaxClassId,
                Manufacturer = product.Manufacturer != null ? new ManufacturerDetailDto
                {
                    Id = product.Manufacturer.Id,
                    Name = product.Manufacturer.Name,
                    Street = product.Manufacturer.Street,
                    City = product.Manufacturer.City,
                    State = product.Manufacturer.State,
                    Country = product.Manufacturer.Country,
                    ZipCode = product.Manufacturer.ZipCode,
                    Phone = product.Manufacturer.Phone,
                    Email = product.Manufacturer.Email,
                    Website = product.Manufacturer.Website,
                    Logo = product.Manufacturer.Logo
                } : null,
                // Map related sales channels and stocks
                ProductSalesChannel = product.ProductSalesChannels?.Select(psc => psc.Id).ToList() ?? new List<int>(),
                ProductStocks = product.ProductStocks.Select(ps => ps.Id).ToList()
            };

            // Set successful result with the product details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("Product with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during product retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the product: {ex.Message}");

            _logger.LogError("Error retrieving product: {Message}", ex.Message);
        }

        return result;
    }
}