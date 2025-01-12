using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateValidator : AbstractValidator<OrderUpdateCommand>
{
    private readonly IOrderRepository _orderRepository;

    public OrderUpdateValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(o => o)
            .MustAsync(OrderExists).WithMessage("Order not found");
    }
    
    private async Task<bool> OrderExists(OrderUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _orderRepository.ExistsAsync(command.Id);
    }
}