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
        
        RuleFor(p => p)
            .MustAsync(ProductExists).WithMessage("Product does not exist.");
    }
    
    private async Task<bool> ProductExists(ProductDeleteCommand command, CancellationToken token)
    {
        return await _productRepository.ExistsAsync(command.Id);
    }
}