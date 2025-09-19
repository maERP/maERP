using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteHandler : IRequestHandler<ProductDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<ProductDeleteHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductDeleteHandler(
        IAppLogger<ProductDeleteHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<Guid>> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting product with ID: {Id}", request.Id);

        // Validate incoming data
        var validator = new ProductDeleteValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var validationErrors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(ProductDeleteCommand), validationErrors);

            return Result<Guid>.Fail(ResultStatusCode.BadRequest, validationErrors);
        }

        try
        {
            // Get entity from database first
            var productToDelete = await _productRepository.GetByIdAsync(request.Id);

            if (productToDelete == null)
            {
                _logger.LogWarning("Product with ID: {Id} not found for deletion", request.Id);
                return Result<Guid>.Fail(ResultStatusCode.NotFound, "Product not found");
            }

            // Delete from database
            await _productRepository.DeleteAsync(productToDelete);

            _logger.LogInformation("Successfully deleted product with ID: {Id}", productToDelete.Id);
            
            var result = Result<Guid>.Success(productToDelete.Id);
            result.StatusCode = ResultStatusCode.NoContent;
            return result;
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            // Handle concurrent deletion - product was already deleted by another request
            _logger.LogWarning("Product with ID: {Id} was deleted by another request: {Message}", request.Id, ex.Message);
            
            return Result<Guid>.Fail(ResultStatusCode.NotFound, "Product not found");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error deleting product: {Message}", ex.Message);
            
            return Result<Guid>.Fail(ResultStatusCode.InternalServerError,
                $"An error occurred while deleting the product: {ex.Message}");
        }
    }
}