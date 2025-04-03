using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, Result<int>>
{
    private readonly IAppLogger<OrderCreateHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCreateHandler(
        IAppLogger<OrderCreateHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Result<int>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new order with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new OrderCreateValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

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
            // Manuelles Mapping statt AutoMapper
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
                // OrderItems müssten separat gemappt werden
            };
            
            // add to database
            await _orderRepository.CreateAsync(orderToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = orderToCreate.Id;
            
            _logger.LogInformation("Successfully created order with ID: {Id}", orderToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the order: {ex.Message}");
            
            _logger.LogError("Error creating order: {Message}", ex.Message);
        }

        return result;
    }
}