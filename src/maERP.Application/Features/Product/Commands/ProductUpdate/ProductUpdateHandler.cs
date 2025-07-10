using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, Result<int>>
{
    private readonly IAppLogger<ProductUpdateHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductUpdateHandler(
        IAppLogger<ProductUpdateHandler> logger,
        IProductRepository productRepository)
    {
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
            // Manuelles Mapping statt AutoMapper
            var productToUpdate = new Domain.Entities.Product
            {
                Id = request.Id,
                Sku = request.Sku,
                Name = request.Name,
                NameOptimized = request.NameOptimized,
                Ean = request.Ean,
                Asin = request.Asin,
                Description = request.Description,
                DescriptionOptimized = request.DescriptionOptimized,
                UseOptimized = request.UseOptimized,
                Price = request.Price,
                Msrp = request.Msrp,
                Weight = request.Weight,
                Width = request.Width,
                Height = request.Height,
                Depth = request.Depth,
                TaxClassId = request.TaxClassId
            };

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
