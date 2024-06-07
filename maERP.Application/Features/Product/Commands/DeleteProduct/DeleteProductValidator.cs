using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p)
            .MustAsync(ProductExists).WithMessage("Product does not exist.");
    }
    
    private async Task<bool> ProductExists(DeleteProductCommand command, CancellationToken token)
    {
        return await _productRepository.ExistsAsync(command.Id);
    }
}