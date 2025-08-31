using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteValidator : AbstractValidator<ProductDeleteCommand>
{
    private readonly IProductRepository _productRepository;

    public ProductDeleteValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}