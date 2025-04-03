using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand, Result<int>>
{
    private readonly IAppLogger<OrderUpdateHandler> _logger;
    private readonly IOrderRepository _orderRepository;


    public OrderUpdateHandler(
        IAppLogger<OrderUpdateHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Result<int>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating order with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new OrderUpdateValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in update request for {0}: {1}", 
                nameof(OrderUpdateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Manuelles Mapping statt AutoMapper
            var orderToUpdate = new Domain.Entities.Order
            {
                Id = request.Id,
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
                // OrderItems müssten separat gemappt werden
            };
            
            // Update in database
            await _orderRepository.UpdateAsync(orderToUpdate);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = orderToUpdate.Id;
            
            _logger.LogInformation("Successfully updated order with ID: {Id}", orderToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the order: {ex.Message}");
            
            _logger.LogError("Error updating order: {Message}", ex.Message);
        }

        return result;
    }
}
