using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<OrderUpdateHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPdfService _pdfService;


    public OrderUpdateHandler(
        IAppLogger<OrderUpdateHandler> logger,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IInvoiceRepository invoiceRepository,
        IPdfService pdfService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
    }

    public async Task<Result<Guid>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating order with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new OrderUpdateValidator(_orderRepository, _customerRepository);
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
            // First check if order exists globally
            var existsGlobally = await _orderRepository.ExistsGloballyAsync(request.Id);
            if (!existsGlobally)
            {
                // Order doesn't exist at all
                _logger.LogWarning("Order not found: {OrderId}", request.Id);
                throw new NotFoundException("Order", request.Id);
            }

            // Check tenant isolation - order exists globally but might belong to different tenant
            var existsForCurrentTenant = await _orderRepository.ExistsAsync(request.Id);
            if (!existsForCurrentTenant)
            {
                // Order exists globally but not for current tenant - cross-tenant access attempt
                _logger.LogWarning("Cross-tenant access attempt for order {OrderId}", request.Id);
                throw new NotFoundException("Order", request.Id);
            }

            // Validate customer belongs to current tenant
            var customer = await _customerRepository.GetByCustomerIdAsync(request.CustomerId);
            if (customer == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Der angegebene Kunde existiert nicht oder gehört nicht zu Ihrem Tenant.");
                _logger.LogWarning("Cross-tenant customer access attempt for customer {CustomerId}", request.CustomerId);
                return result;
            }

            // Manuelles Mapping statt AutoMapper
            var orderToUpdate = new Domain.Entities.Order
            {
                Id = request.Id,
                OrderId = request.OrderId,
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
                DeliveryAddressZip = request.DeliveryAddressZip,
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
        catch (NotFoundException)
        {
            // Let NotFoundException bubble up to middleware for proper 404 handling
            throw;
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
