using maERP.Application.Contracts.Logging;
using System.Linq;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Invoice.Commands.InvoiceCreate;

/// <summary>
/// Handler for processing invoice creation commands.
/// Implements IRequestHandler from MediatR to handle InvoiceCreateCommand requests
/// and return the ID of the newly created invoice wrapped in a Result.
/// </summary>
public class InvoiceCreateHandler : IRequestHandler<InvoiceCreateCommand, Result<Guid>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<InvoiceCreateHandler> _logger;

    /// <summary>
    /// Repository for invoice data operations
    /// </summary>
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ITenantContext _tenantContext;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceCreateHandler(
        IAppLogger<InvoiceCreateHandler> logger,
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        ITenantContext tenantContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    /// <summary>
    /// Handles the invoice creation request
    /// </summary>
    /// <param name="request">The invoice creation command with invoice details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created invoice if successful</returns>
    public async Task<Result<Guid>> Handle(InvoiceCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new invoice with number: {InvoiceNumber}", request.InvoiceNumber);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new InvoiceCreateValidator(_invoiceRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(InvoiceCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            var currentTenantId = _tenantContext.GetCurrentTenantId();
            if (!currentTenantId.HasValue || currentTenantId == Guid.Empty)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Ein Mandantenkontext ist erforderlich.");
                return result;
            }

            var assignedTenantIds = _tenantContext.GetAssignedTenantIds();
            if (assignedTenantIds.Count > 0 && !assignedTenantIds.Contains(currentTenantId.Value))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Mandant wurde nicht gefunden oder ist nicht zugewiesen.");
                return result;
            }

            var customer = await _customerRepository.GetByCustomerIdAsync(request.CustomerId);
            if (customer == null || customer.TenantId != currentTenantId.Value)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Kunde wurde nicht gefunden oder gehört zu einem anderen Mandanten.");
                return result;
            }

            if (request.OrderId.HasValue)
            {
                var relatedOrder = await _orderRepository.GetByIdAsync(request.OrderId.Value);
                if (relatedOrder == null || relatedOrder.TenantId != currentTenantId.Value)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Bestellung wurde nicht gefunden oder gehört zu einem anderen Mandanten.");
                    return result;
                }

                if (relatedOrder.CustomerId != request.CustomerId)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Die Bestellung gehört nicht zum ausgewählten Kunden.");
                    return result;
                }
            }

            // Manual mapping instead of using AutoMapper
            var invoiceToCreate = new Domain.Entities.Invoice
            {
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                CustomerId = request.CustomerId,
                OrderId = request.OrderId,
                Subtotal = request.Subtotal,
                ShippingCost = request.ShippingCost,
                TotalTax = request.TotalTax,
                Total = request.Total,
                PaymentStatus = request.PaymentStatus,
                InvoiceStatus = request.InvoiceStatus,
                PaymentMethod = request.PaymentMethod,
                PaymentTransactionId = request.PaymentTransactionId,
                Notes = request.Notes,
                InvoiceAddressFirstName = request.InvoiceAddressFirstName,
                InvoiceAddressLastName = request.InvoiceAddressLastName,
                InvoiceAddressCompanyName = request.InvoiceAddressCompanyName,
                InvoiceAddressPhone = request.InvoiceAddressPhone,
                InvoiceAddressStreet = request.InvoiceAddressStreet,
                InvoiceAddressCity = request.InvoiceAddressCity,
                InvoiceAddressZip = request.InvoiceAddressZip,
                InvoiceAddressCountry = request.InvoiceAddressCountry,
                DeliveryAddressFirstName = request.DeliveryAddressFirstName,
                DeliveryAddressLastName = request.DeliveryAddressLastName,
                DeliveryAddressCompanyName = request.DeliveryAddressCompanyName,
                DeliveryAddressPhone = request.DeliveryAddressPhone,
                DeliveryAddressStreet = request.DeliveryAddressStreet,
                DeliveryAddressCity = request.DeliveryAddressCity,
                DeliveryAddressZip = request.DeliveryAddressZip,
                DeliveryAddressCountry = request.DeliveryAddressCountry,
                TenantId = currentTenantId.Value
                // InvoiceItems would need to be mapped separately
            };

            // Add the new invoice to the database
            await _invoiceRepository.CreateAsync(invoiceToCreate);

            // Set successful result with the new invoice ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = invoiceToCreate.Id;

            _logger.LogInformation("Successfully created invoice with ID: {Id}", invoiceToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during invoice creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the invoice: {ex.Message}");

            _logger.LogError("Error creating invoice: {Message}", ex.Message);
        }

        return result;
    }
}
