using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

/// <summary>
/// Handler for processing order detail queries.
/// Implements IRequestHandler from MediatR to handle OrderDetailQuery requests
/// and return detailed order information wrapped in a Result.
/// </summary>
public class OrderDetailHandler : IRequestHandler<OrderDetailQuery, Result<OrderDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<OrderDetailHandler> _logger;
    
    /// <summary>
    /// Repository for order data operations
    /// </summary>
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="orderRepository">Repository for order data access</param>
    public OrderDetailHandler(
        IAppLogger<OrderDetailHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }
    
    /// <summary>
    /// Handles the order detail query request
    /// </summary>
    /// <param name="request">The query containing the order ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed order information if successful</returns>
    public async Task<Result<OrderDetailDto>> Handle(OrderDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving order details for ID: {Id}", request.Id);
        
        var result = new Result<OrderDetailDto>();
        
        try
        {
            // Retrieve order with all related details from the repository
            var order = await _orderRepository.GetWithDetailsAsync(request.Id);

            // If order not found, return a not found result
            if (order == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Order with ID {request.Id} not found");
                
                _logger.LogWarning("Order with ID {Id} not found", request.Id);
                return result;
            }

            // Bestellhistorie abrufen
            var orderHistory = await _orderRepository.GetOrderHistoryAsync(request.Id);

            // Manual mapping from entity to DTO
            var data = new OrderDetailDto
            {
                Id = order.Id,
                SalesChannelId = order.SalesChannelId,
                RemoteOrderId = order.RemoteOrderId,
                CustomerId = order.CustomerId,
                Status = order.Status,
                OrderItems = order.OrderItems.ToList(),
                OrderHistory = MapOrderHistoryToDto(orderHistory),
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                PaymentProvider = order.PaymentProvider,
                PaymentTransactionId = order.PaymentTransactionId,
                // Shipping fields not available in the entity, using empty values
                ShippingMethod = string.Empty,
                ShippingStatus = string.Empty,
                ShippingProvider = string.Empty,
                ShippingTrackingId = string.Empty,
                Subtotal = order.Subtotal,
                ShippingCost = order.ShippingCost,
                TotalTax = order.TotalTax,
                Total = order.Total,
                Note = order.CustomerNote,
                // Delivery address details
                DeliveryAddressFirstName = order.DeliveryAddressFirstName,
                DeliveryAddressLastName = order.DeliveryAddressLastName,
                DeliveryAddressCompanyName = order.DeliveryAddressCompanyName,
                DeliveryAddressPhone = order.DeliveryAddressPhone,
                DeliveryAddressStreet = order.DeliveryAddressStreet,
                DeliveryAddressCity = order.DeliveryAddressCity,
                DeliverAddressZip = order.DeliverAddressZip,
                DeliveryAddressCountry = order.DeliveryAddressCountry,
                // Invoice address details
                InvoiceAddressFirstName = order.InvoiceAddressFirstName,
                InvoiceAddressLastName = order.InvoiceAddressLastName,
                InvoiceAddressCompanyName = order.InvoiceAddressCompanyName,
                InvoiceAddressPhone = order.InvoiceAddressPhone,
                InvoiceAddressStreet = order.InvoiceAddressStreet,
                InvoiceAddressCity = order.InvoiceAddressCity,
                InvoiceAddressZip = order.InvoiceAddressZip,
                InvoiceAddressCountry = order.InvoiceAddressCountry,
                DateOrdered = order.DateOrdered
            };

            // Set successful result with the order details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("Order with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during order retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the order: {ex.Message}");
            
            _logger.LogError("Error retrieving order: {Message}", ex.Message);
        }
        
        return result;
    }

    /// <summary>
    /// Konvertiert OrderHistory-Entities in OrderHistoryDto-Objekte
    /// </summary>
    /// <param name="orderHistories">Liste von OrderHistory-Entities</param>
    /// <returns>Liste von OrderHistoryDto-Objekten</returns>
    private List<OrderHistoryDto> MapOrderHistoryToDto(List<Domain.Entities.OrderHistory> orderHistories)
    {
        return orderHistories.Select(history => new OrderHistoryDto
        {
            Id = history.Id,
            OrderId = history.OrderId,
            Timestamp = history.Timestamp,
            Action = history.FieldName,
            PreviousStatus = history.OldStatus,
            NewStatus = history.NewStatus,
            Description = history.Comment,
            CreatedBy = history.Username,
            IsSystemGenerated = string.IsNullOrEmpty(history.Username)
        }).ToList();
    }
}