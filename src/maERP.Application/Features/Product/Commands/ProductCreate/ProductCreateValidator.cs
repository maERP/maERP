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
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="productRepository">Repository for product data access</param>
    public ProductCreateValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        // Add rule to check if the product SKU is unique before creating
        RuleFor(p => p)
            .MustAsync(ProductUnique).WithMessage("Product with the same SKU already exists.");
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
}