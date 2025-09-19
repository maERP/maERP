using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating product with ID: {Id}", request.Id);

        // Validate incoming data
        var validator = new ProductUpdateValidator(_productRepository, _taxClassRepository, _manufacturerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var validationErrors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(ProductUpdateCommand), validationErrors);

            // Check if the validation error is about product not found
            if (validationResult.Errors.Any(e => e.ErrorMessage.Contains("Product not found")))
            {
                return Result<Guid>.Fail(ResultStatusCode.NotFound, validationErrors);
            }
            
            return Result<Guid>.Fail(ResultStatusCode.BadRequest, validationErrors);
        }

        try
        {
            // Load existing product from database
            var productToUpdate = await _productRepository.GetByIdAsync(request.Id);

            if (productToUpdate == null)
            {
                _logger.LogWarning("Product with ID {Id} not found for update", request.Id);
                return Result<Guid>.Fail(ResultStatusCode.NotFound, "Product not found.");
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

            _logger.LogInformation("Successfully updated product with ID: {Id}", productToUpdate.Id);
            
            return Result<Guid>.Success(productToUpdate.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating product: {Message}", ex.Message);
            
            return Result<Guid>.Fail(ResultStatusCode.InternalServerError,
                $"An error occurred while updating the product: {ex.Message}");
        }
    }
}
