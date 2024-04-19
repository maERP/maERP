using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Order.Commands.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");
    }

    private async Task<bool> OrderUnique(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique Order name validation
        await Task.CompletedTask;
        return true;
    }
}
