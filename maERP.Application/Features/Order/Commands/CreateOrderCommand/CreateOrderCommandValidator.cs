using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Order.Commands.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
        
        RuleFor(q => q)
            .MustAsync(OrderUnique).WithMessage("Order with the same name already exists.");
    }

    private async Task<bool> OrderUnique(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByIdAsync(command.Id) != null;
    }
}
