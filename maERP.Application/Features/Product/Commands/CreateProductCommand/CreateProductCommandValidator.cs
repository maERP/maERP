using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Product.Commands.CreateProductCommand;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(3).WithMessage("{PropertyName} must be more than {MinLength} characters.")
            .MaximumLength(255).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
    }

    private async Task<bool> ProductUnique(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique Product name validation
        await Task.CompletedTask;
        return true;
    }
}
