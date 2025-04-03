using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

public class OrderDetailHandler : IRequestHandler<OrderDetailQuery, Result<OrderDetailDto>>
{
    private readonly IAppLogger<OrderDetailHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderDetailHandler(
        IAppLogger<OrderDetailHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }
    
    public async Task<Result<OrderDetailDto>> Handle(OrderDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving order details for ID: {Id}", request.Id);
        
        var result = new Result<OrderDetailDto>();
        
        try
        {
            var order = await _orderRepository.GetWithDetailsAsync(request.Id);

            if (order == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Order with ID {request.Id} not found");
                
                _logger.LogWarning("Order with ID {Id} not found", request.Id);
                return result;
            }

            var data = new OrderDetailDto
            {
                Id = order.Id,
                SalesChannelId = order.SalesChannelId,
                RemoteOrderId = order.RemoteOrderId,
                CustomerId = order.CustomerId,
                Status = order.Status,
                OrderItems = order.OrderItems.ToList(),
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                PaymentProvider = order.PaymentProvider,
                PaymentTransactionId = order.PaymentTransactionId,
                ShippingMethod = string.Empty,
                ShippingStatus = string.Empty,
                ShippingProvider = string.Empty,
                ShippingTrackingId = string.Empty,
                Subtotal = order.Subtotal,
                ShippingCost = order.ShippingCost,
                TotalTax = order.TotalTax,
                Total = order.Total,
                Note = order.CustomerNote,
                DeliveryAddressFirstName = order.DeliveryAddressFirstName,
                DeliveryAddressLastName = order.DeliveryAddressLastName,
                DeliveryAddressCompanyName = order.DeliveryAddressCompanyName,
                DeliveryAddressPhone = order.DeliveryAddressPhone,
                DeliveryAddressStreet = order.DeliveryAddressStreet,
                DeliveryAddressCity = order.DeliveryAddressCity,
                DeliverAddressZip = order.DeliverAddressZip,
                DeliveryAddressCountry = order.DeliveryAddressCountry,
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

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("Order with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the order: {ex.Message}");
            
            _logger.LogError("Error retrieving order: {Message}", ex.Message);
        }
        
        return result;
    }
}