using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

/// <summary>
/// Validator for order creation commands.
/// Extends OrderBaseValidator to inherit common validation rules for order data
/// and adds specific validation for order creation operations.
/// </summary>
public class OrderCreateValidator : OrderBaseValidator<OrderCreateCommand>
{
    /// <summary>
    /// Repository for order data operations
    /// </summary>
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="orderRepository">Repository for order data access</param>
    public OrderCreateValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        /*
        // Commented out uniqueness check - would be used if orders needed to be unique
        RuleFor(q => q)
            .MustAsync(OrderUniqueAsync).WithMessage("Order with the same values already exists.");
        */
    }

    /// <summary>
    /// Asynchronously checks if an order with the same values already exists in the database.
    /// Currently not used but kept for potential future use.
    /// </summary>
    /// <param name="command">The order creation command to validate</param>
    /// <returns>True if the order is unique, false otherwise</returns>
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