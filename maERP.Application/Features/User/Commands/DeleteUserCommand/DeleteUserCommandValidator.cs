using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.Product.Commands.DeleteProductCommand;

namespace maERP.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommandValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteUserCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}
