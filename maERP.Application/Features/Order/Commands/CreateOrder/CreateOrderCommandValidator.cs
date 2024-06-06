using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        
        /*
        RuleFor(q => q)
            .MustAsync(OrderUniqueAsync).WithMessage("Order with the same values already exists.");
        */
    }

    private async Task<bool> OrderUniqueAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderToCreate = new Domain.Models.Order
        {
             
        };

        return await _orderRepository.IsUniqueAsync(orderToCreate);
    }
}