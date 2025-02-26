using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<ProductUpdateHandler> _logger;
    private readonly IProductRepository _productRepository;


    public ProductUpdateHandler(IMapper mapper,
        IAppLogger<ProductUpdateHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<int>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating product with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new ProductUpdateValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in update request for {0}: {1}", 
                nameof(ProductUpdateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Map to domain entity
            var productToUpdate = _mapper.Map<Domain.Entities.Product>(request);
            
            // Update in database
            await _productRepository.UpdateAsync(productToUpdate);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = productToUpdate.Id;
            
            _logger.LogInformation("Successfully updated product with ID: {Id}", productToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the product: {ex.Message}");
            
            _logger.LogError("Error updating product: {Message}", ex.Message);
        }

        return result;
    }
}
