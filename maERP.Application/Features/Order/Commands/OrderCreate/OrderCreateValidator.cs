using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
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

    private async Task<bool> OrderUniqueAsync(OrderCreateCommand command, CancellationToken cancellationToken)
    {
        var orderToCreate = new Domain.Models.Order
        {
             
        };

        return await _orderRepository.IsUniqueAsync(orderToCreate);
    }
}