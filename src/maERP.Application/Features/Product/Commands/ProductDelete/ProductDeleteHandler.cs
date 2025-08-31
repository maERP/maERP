using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteHandler : IRequestHandler<ProductDeleteCommand, Result<int>>
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

    public async Task<Result<int>> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting product with ID: {Id}", request.Id);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new ProductDeleteValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(ProductDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Get entity from database first
            var productToDelete = await _productRepository.GetByIdAsync(request.Id);

            if (productToDelete == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Product not found");

                _logger.LogWarning("Product with ID: {Id} not found for deletion", request.Id);
                return result;
            }

            // Delete from database
            await _productRepository.DeleteAsync(productToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = productToDelete.Id;

            _logger.LogInformation("Successfully deleted product with ID: {Id}", productToDelete.Id);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            // Handle concurrent deletion - product was already deleted by another request
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add("Product not found");

            _logger.LogWarning("Product with ID: {Id} was deleted by another request: {Message}", request.Id, ex.Message);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the product: {ex.Message}");

            _logger.LogError("Error deleting product: {Message}", ex.Message);
        }

        return result;
    }
}