using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Order.Commands.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(o => o)
            .MustAsync(OrderExists).WithMessage("Order not found");
    }
    
    private async Task<bool> OrderExists(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        return await _orderRepository.ExistsAsync(command.Id);
    }
}
