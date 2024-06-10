using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Product.Commands.ProductCreate;

public class ProductCreateValidator : AbstractValidator<ProductCreateCommand>
{
    private readonly IProductRepository _productRepository;

    public ProductCreateValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.Sku)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(3).WithMessage("{PropertyName} must be more than {MinLength} characters.")
            .MaximumLength(255).WithMessage("{PropertyName} must be less than {MaxLength} characters.");

        RuleFor(p => p)
            .MustAsync(ProductUnique).WithMessage("Product with the same SKU already exists.");
    }

    private async Task<bool> ProductUnique(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        return await _productRepository.GetBySkuAsync(command.Sku) == null;
    }
}