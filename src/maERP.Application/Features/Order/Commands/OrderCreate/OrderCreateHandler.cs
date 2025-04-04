using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

/// <summary>
/// Handler for processing order creation commands.
/// Implements IRequestHandler from MediatR to handle OrderCreateCommand requests
/// and return the ID of the newly created order wrapped in a Result.
/// </summary>
public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, Result<int>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<OrderCreateHandler> _logger;
    
    /// <summary>
    /// Repository for order data operations
    /// </summary>
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="orderRepository">Repository for order data access</param>
    public OrderCreateHandler(
        IAppLogger<OrderCreateHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    /// <summary>
    /// Handles the order creation request
    /// </summary>
    /// <param name="request">The order creation command with order details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created order if successful</returns>
    public async Task<Result<int>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new order with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new OrderCreateValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(OrderCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Manual mapping instead of using AutoMapper
            var orderToCreate = new Domain.Entities.Order
            {
                SalesChannelId = request.SalesChannelId,
                RemoteOrderId = request.RemoteOrderId,
                CustomerId = request.CustomerId,
                Status = request.Status,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentStatus,
                PaymentProvider = request.PaymentProvider,
                PaymentTransactionId = request.PaymentTransactionId,
                CustomerNote = request.CustomerNote,
                InternalNote = request.InternalNote,
                Subtotal = request.Subtotal,
                ShippingCost = request.ShippingCost,
                TotalTax = request.TotalTax,
                Total = request.Total,
                DeliveryAddressFirstName = request.DeliveryAddressFirstName,
                DeliveryAddressLastName = request.DeliveryAddressLastName,
                DeliveryAddressCompanyName = request.DeliveryAddressCompanyName,
                DeliveryAddressPhone = request.DeliveryAddressPhone,
                DeliveryAddressStreet = request.DeliveryAddressStreet,
                DeliveryAddressCity = request.DeliveryAddressCity,
                DeliverAddressZip = request.DeliverAddressZip,
                DeliveryAddressCountry = request.DeliveryAddressCountry,
                InvoiceAddressFirstName = request.InvoiceAddressFirstName,
                InvoiceAddressLastName = request.InvoiceAddressLastName,
                InvoiceAddressCompanyName = request.InvoiceAddressCompanyName,
                InvoiceAddressPhone = request.InvoiceAddressPhone,
                InvoiceAddressStreet = request.InvoiceAddressStreet,
                InvoiceAddressCity = request.InvoiceAddressCity,
                InvoiceAddressZip = request.InvoiceAddressZip,
                InvoiceAddressCountry = request.InvoiceAddressCountry,
                DateOrdered = request.DateOrdered
                // OrderItems would need to be mapped separately
            };
            
            // Add the new order to the database
            await _orderRepository.CreateAsync(orderToCreate);

            // Set successful result with the new order ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = orderToCreate.Id;
            
            _logger.LogInformation("Successfully created order with ID: {Id}", orderToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during order creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the order: {ex.Message}");
            
            _logger.LogError("Error creating order: {Message}", ex.Message);
        }

        return result;
    }
}