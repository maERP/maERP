using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, Result<int>>
{
    private readonly IAppLogger<ProductUpdateHandler> _logger;
    private readonly IProductRepository _productRepository;
    private readonly ITaxClassRepository _taxClassRepository;
    private readonly IManufacturerRepository _manufacturerRepository;

    public ProductUpdateHandler(
        IAppLogger<ProductUpdateHandler> logger,
        IProductRepository productRepository,
        ITaxClassRepository taxClassRepository,
        IManufacturerRepository manufacturerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
        _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
    }

    public async Task<Result<int>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating product with ID: {Id}", request.Id);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new ProductUpdateValidator(_productRepository, _taxClassRepository, _manufacturerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;

            // Check if the validation error is about invalid ID (0 or negative)
            if (validationResult.Errors.Any(e => e.PropertyName == "Id" && 
                (e.ErrorMessage.Contains("must be greater than 0") || 
                 e.ErrorMessage.Contains("is required"))))
            {
                result.StatusCode = ResultStatusCode.BadRequest;
            }
            // Check if the validation error is about product not found
            else if (validationResult.Errors.Any(e => e.ErrorMessage.Contains("Product not found")))
            {
                result.StatusCode = ResultStatusCode.NotFound;
            }
            else
            {
                result.StatusCode = ResultStatusCode.BadRequest;
            }

            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(ProductUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Load existing product from database
            var productToUpdate = await _productRepository.GetByIdAsync(request.Id);

            if (productToUpdate == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Product not found.");
                
                _logger.LogWarning("Product with ID {Id} not found for update", request.Id);
                return result;
            }

            // Update properties
            productToUpdate.Sku = request.Sku;
            productToUpdate.Name = request.Name;
            productToUpdate.NameOptimized = request.NameOptimized;
            productToUpdate.Ean = request.Ean;
            productToUpdate.Asin = request.Asin;
            productToUpdate.Description = request.Description;
            productToUpdate.DescriptionOptimized = request.DescriptionOptimized;
            productToUpdate.UseOptimized = request.UseOptimized;
            productToUpdate.Price = request.Price;
            productToUpdate.Msrp = request.Msrp;
            productToUpdate.Weight = request.Weight;
            productToUpdate.Width = request.Width;
            productToUpdate.Height = request.Height;
            productToUpdate.Depth = request.Depth;
            productToUpdate.TaxClassId = request.TaxClassId;
            productToUpdate.ManufacturerId = request.ManufacturerId;

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
