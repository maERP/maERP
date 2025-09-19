using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;
using FluentValidation;

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
    /// Repository for customer data operations
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="orderRepository">Repository for order data access</param>
    /// <param name="customerRepository">Repository for customer data access</param>
    public OrderCreateValidator(IOrderRepository orderRepository, ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;

        // Add validation rule to ensure customer exists and belongs to current tenant
        RuleFor(x => x.CustomerId)
            .MustAsync(CustomerExistsAndBelongsToCurrentTenant)
            .WithMessage("Der angegebene Kunde existiert nicht oder gehört nicht zu Ihrem Tenant.");

        /*
        // Commented out uniqueness check - would be used if orders needed to be unique
        RuleFor(q => q)
            .MustAsync(OrderUniqueAsync).WithMessage("Order with the same values already exists.");
        */
    }

    /// <summary>
    /// Asynchronously checks if a customer with the specified ID exists and belongs to the current tenant.
    /// Uses EF Core Global Query Filter to ensure tenant isolation.
    /// </summary>
    /// <param name="customerId">The customer ID to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the customer exists and belongs to current tenant, false otherwise</returns>
    private async Task<bool> CustomerExistsAndBelongsToCurrentTenant(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        return customer != null; // EF Core Global Query Filter ensures tenant isolation
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