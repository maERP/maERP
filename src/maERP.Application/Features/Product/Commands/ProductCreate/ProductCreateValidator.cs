using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Product.Commands.ProductCreate;

/// <summary>
/// Validator for product creation commands.
/// Extends ProductBaseValidator to inherit common validation rules for product data
/// and adds specific validation for product creation operations.
/// </summary>
public class ProductCreateValidator : ProductBaseValidator<ProductCreateCommand>
{
    /// <summary>
    /// Repository for product data operations
    /// </summary>
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Repository for tax class data operations
    /// </summary>
    private readonly ITaxClassRepository _taxClassRepository;

    /// <summary>
    /// Repository for manufacturer data operations
    /// </summary>
    private readonly IManufacturerRepository _manufacturerRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="productRepository">Repository for product data access</param>
    /// <param name="taxClassRepository">Repository for tax class data access</param>
    /// <param name="manufacturerRepository">Repository for manufacturer data access</param>
    public ProductCreateValidator(
        IProductRepository productRepository, 
        ITaxClassRepository taxClassRepository, 
        IManufacturerRepository manufacturerRepository)
    {
        _productRepository = productRepository;
        _taxClassRepository = taxClassRepository;
        _manufacturerRepository = manufacturerRepository;

        // Add rule to check if the product SKU is unique before creating
        RuleFor(p => p)
            .MustAsync(ProductUnique).WithMessage("Product with the same SKU already exists.");

        // Add rule to check if the tax class exists
        RuleFor(p => p.TaxClassId)
            .MustAsync(TaxClassExists).WithMessage("Tax class does not exist.");

        // Add rule to check if the manufacturer exists (when not null)
        RuleFor(p => p.ManufacturerId)
            .MustAsync(ManufacturerExists).WithMessage("Manufacturer does not exist.")
            .When(p => p.ManufacturerId.HasValue);
    }

    /// <summary>
    /// Asynchronously checks if a product with the same SKU already exists in the database
    /// </summary>
    /// <param name="command">The product creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product SKU is unique, false otherwise</returns>
    private async Task<bool> ProductUnique(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        return await _productRepository.GetBySkuAsync(command.Sku) == null;
    }

    /// <summary>
    /// Asynchronously checks if the tax class exists in the database
    /// </summary>
    /// <param name="taxClassId">The tax class ID to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the tax class exists, false otherwise</returns>
    private async Task<bool> TaxClassExists(Guid taxClassId, CancellationToken cancellationToken)
    {
        return await _taxClassRepository.ExistsAsync(taxClassId);
    }

    /// <summary>
    /// Asynchronously checks if the manufacturer exists in the database
    /// </summary>
    /// <param name="manufacturerId">The manufacturer ID to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the manufacturer exists, false otherwise</returns>
    private async Task<bool> ManufacturerExists(Guid? manufacturerId, CancellationToken cancellationToken)
    {
        if (!manufacturerId.HasValue)
            return true;

        return await _manufacturerRepository.ExistsAsync(manufacturerId.Value);
    }
}