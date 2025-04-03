using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

public class ProductDetailHandler : IRequestHandler<ProductDetailQuery, Result<ProductDetailDto>>
{
    private readonly IAppLogger<ProductDetailHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductDetailHandler(
        IAppLogger<ProductDetailHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }
    
    public async Task<Result<ProductDetailDto>> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving product details for ID: {Id}", request.Id);
        
        var result = new Result<ProductDetailDto>();
        
        try
        {
            var product = await _productRepository.GetByIdAsync(request.Id, true);

            if (product == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Product with ID {request.Id} not found");
                
                _logger.LogWarning("Product with ID {Id} not found", request.Id);
                return result;
            }

            // Manuelles Mapping statt AutoMapper
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
                ProductSalesChannel = product.ProductSalesChannels?.Select(psc => psc.Id).ToList() ?? new List<int>(),
                ProductStocks = product.ProductStocks.Select(ps => ps.Id).ToList()
            };

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("Product with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the product: {ex.Message}");
            
            _logger.LogError("Error retrieving product: {Message}", ex.Message);
        }
        
        return result;
    }
}