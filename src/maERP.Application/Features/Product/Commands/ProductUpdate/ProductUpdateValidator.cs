using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateValidator : ProductBaseValidator<ProductUpdateCommand>
{
    private readonly IProductRepository _productRepository;

    public ProductUpdateValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p)
            .MustAsync(ProductExists).WithMessage("Product does not exist.");
    }

    private async Task<bool> ProductExists(ProductUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(command.Id, true) != null;
    }
}