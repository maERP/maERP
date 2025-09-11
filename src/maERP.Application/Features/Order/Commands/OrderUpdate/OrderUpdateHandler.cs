using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<OrderUpdateHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPdfService _pdfService;


    public OrderUpdateHandler(
        IAppLogger<OrderUpdateHandler> logger,
        IOrderRepository orderRepository,
        IInvoiceRepository invoiceRepository,
        IPdfService pdfService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
    }

    public async Task<Result<Guid>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating order with ID: {Id}", request.Id);

        var result = new Result<Guid>();

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

            // Prüfen, ob eine Rechnung erstellt werden kann
            bool canCreateInvoice = await _orderRepository.CanCreateInvoice(orderToUpdate.Id);

            if (canCreateInvoice)
            {
                try
                {
                    // Bestellung mit Details laden
                    var orderWithDetails = await _orderRepository.GetWithDetailsAsync(orderToUpdate.Id);

                    if (orderWithDetails != null)
                    {
                        // Rechnung erstellen mit der ausgelagerten Methode
                        await _invoiceRepository.CreateInvoiceFromOrderAsync(orderWithDetails);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error creating invoice for order ID {Id}: {Message}", orderToUpdate.Id, ex.Message);
                }
            }

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
