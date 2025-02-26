using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

public class ProductDetailHandler : IRequestHandler<ProductDetailQuery, Result<ProductDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<ProductDetailHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductDetailHandler(IMapper mapper,
        IAppLogger<ProductDetailHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            var data = _mapper.Map<ProductDetailDto>(product);

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