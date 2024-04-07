using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.Product.Commands.CreateProductCommand;

namespace maERP.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateUserCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");
    }

    private async Task<bool> ProductUnique(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique Product name validation
        await Task.CompletedTask;
        return true;
    }
}
