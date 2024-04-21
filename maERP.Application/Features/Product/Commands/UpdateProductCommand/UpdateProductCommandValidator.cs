using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandValidator(IProductRepository productRepository)
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
    
    private async Task<bool> ProductExists(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(command.Id) != null;
    }
}