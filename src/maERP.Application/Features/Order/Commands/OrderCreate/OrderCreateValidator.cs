using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateValidator : OrderBaseValidator<OrderCreateCommand>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCreateValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        
        /*
        RuleFor(q => q)
            .MustAsync(OrderUniqueAsync).WithMessage("Order with the same values already exists.");
        */
    }

    // ReSharper disable once UnusedMember.Local
    private async Task<bool> OrderUniqueAsync(OrderCreateCommand command)
    {
        var orderToCreate = new Domain.Entities.Order
        {
             Id = command.Id
        };

        return await _orderRepository.IsUniqueAsync(orderToCreate);
    }
}