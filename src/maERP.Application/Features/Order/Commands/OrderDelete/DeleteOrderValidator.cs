using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Order.Commands.OrderDelete;

public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(o => o)
            .MustAsync(OrderExists).WithMessage("Order not found");
    }

    private async Task<bool> OrderExists(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        return await _orderRepository.ExistsAsync(command.Id);
    }
}
