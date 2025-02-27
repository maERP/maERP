using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Product.Commands.ProductCreate;

public class ProductCreateValidator : ProductBaseValidator<ProductCreateCommand>
{
    private readonly IProductRepository _productRepository;

    public ProductCreateValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p)
            .MustAsync(ProductUnique).WithMessage("Product with the same SKU already exists.");
    }

    private async Task<bool> ProductUnique(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        return await _productRepository.GetBySkuAsync(command.Sku) == null;
    }
}