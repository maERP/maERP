using maERP.Application.Contracts.Logging;
using System.Linq;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Invoice.Commands.InvoiceUpdate;

/// <summary>
/// Handler for processing invoice update commands.
/// Implements IRequestHandler from MediatR to handle InvoiceUpdateCommand requests
/// and return the ID of the updated invoice wrapped in a Result.
/// </summary>
public class InvoiceUpdateHandler : IRequestHandler<InvoiceUpdateCommand, Result<Guid>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<InvoiceUpdateHandler> _logger;

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
    public InvoiceUpdateHandler(
        IAppLogger<InvoiceUpdateHandler> logger,
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
    /// Handles the invoice update request
    /// </summary>
    /// <param name="request">The invoice update command with invoice details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the updated invoice if successful</returns>
    public async Task<Result<Guid>> Handle(InvoiceUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating invoice with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new InvoiceUpdateValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(InvoiceUpdateCommand),
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

            var invoiceToUpdate = await _invoiceRepository.GetByIdAsync(request.Id);
            if (invoiceToUpdate == null || invoiceToUpdate.TenantId != currentTenantId.Value)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Rechnung wurde nicht gefunden.");
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
                var order = await _orderRepository.GetByIdAsync(request.OrderId.Value);
                if (order == null || order.TenantId != currentTenantId.Value)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Bestellung wurde nicht gefunden oder gehört zu einem anderen Mandanten.");
                    return result;
                }

                if (order.CustomerId != request.CustomerId)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Die Bestellung gehört nicht zum ausgewählten Kunden.");
                    return result;
                }
            }

            var existingInvoices = await _invoiceRepository.GetAllAsync();
            var duplicateInvoiceNumber = existingInvoices.Any(i => i.Id != invoiceToUpdate.Id && i.InvoiceNumber == request.InvoiceNumber && i.TenantId == currentTenantId.Value);
            if (duplicateInvoiceNumber)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Eine Rechnung mit dieser Nummer existiert bereits.");
                return result;
            }

            invoiceToUpdate.InvoiceNumber = request.InvoiceNumber;
            invoiceToUpdate.InvoiceDate = request.InvoiceDate;
            invoiceToUpdate.CustomerId = request.CustomerId;
            invoiceToUpdate.OrderId = request.OrderId;
            invoiceToUpdate.Subtotal = request.Subtotal;
            invoiceToUpdate.ShippingCost = request.ShippingCost;
            invoiceToUpdate.TotalTax = request.TotalTax;
            invoiceToUpdate.Total = request.Total;
            invoiceToUpdate.PaymentStatus = request.PaymentStatus;
            invoiceToUpdate.InvoiceStatus = request.InvoiceStatus;
            invoiceToUpdate.PaymentMethod = request.PaymentMethod;
            invoiceToUpdate.PaymentTransactionId = request.PaymentTransactionId;
            invoiceToUpdate.Notes = request.Notes;
            invoiceToUpdate.InvoiceAddressFirstName = request.InvoiceAddressFirstName;
            invoiceToUpdate.InvoiceAddressLastName = request.InvoiceAddressLastName;
            invoiceToUpdate.InvoiceAddressCompanyName = request.InvoiceAddressCompanyName;
            invoiceToUpdate.InvoiceAddressPhone = request.InvoiceAddressPhone;
            invoiceToUpdate.InvoiceAddressStreet = request.InvoiceAddressStreet;
            invoiceToUpdate.InvoiceAddressCity = request.InvoiceAddressCity;
            invoiceToUpdate.InvoiceAddressZip = request.InvoiceAddressZip;
            invoiceToUpdate.InvoiceAddressCountry = request.InvoiceAddressCountry;
            invoiceToUpdate.DeliveryAddressFirstName = request.DeliveryAddressFirstName;
            invoiceToUpdate.DeliveryAddressLastName = request.DeliveryAddressLastName;
            invoiceToUpdate.DeliveryAddressCompanyName = request.DeliveryAddressCompanyName;
            invoiceToUpdate.DeliveryAddressPhone = request.DeliveryAddressPhone;
            invoiceToUpdate.DeliveryAddressStreet = request.DeliveryAddressStreet;
            invoiceToUpdate.DeliveryAddressCity = request.DeliveryAddressCity;
            invoiceToUpdate.DeliveryAddressZip = request.DeliveryAddressZip;
            invoiceToUpdate.DeliveryAddressCountry = request.DeliveryAddressCountry;

            await _invoiceRepository.UpdateAsync(invoiceToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = invoiceToUpdate.Id;

            _logger.LogInformation("Successfully updated invoice with ID: {Id}", invoiceToUpdate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during invoice update
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the invoice: {ex.Message}");

            _logger.LogError("Error updating invoice: {Message}", ex.Message);
        }

        return result;
    }
}
