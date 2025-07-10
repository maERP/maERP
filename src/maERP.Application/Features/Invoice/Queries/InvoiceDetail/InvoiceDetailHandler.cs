using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Invoice.Queries.InvoiceDetail;

/// <summary>
/// Handler for processing invoice detail queries.
/// Implements IRequestHandler from MediatR to handle InvoiceDetailQuery requests
/// and return detailed invoice information wrapped in a Result.
/// </summary>
public class InvoiceDetailHandler : IRequestHandler<InvoiceDetailQuery, Result<InvoiceDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<InvoiceDetailHandler> _logger;

    /// <summary>
    /// Repository for invoice data operations
    /// </summary>
    private readonly IInvoiceRepository _invoiceRepository;

    /// <summary>
    /// Repository for customer data operations
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    /// <param name="customerRepository">Repository for customer data access</param>
    public InvoiceDetailHandler(
        IAppLogger<InvoiceDetailHandler> logger,
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    /// <summary>
    /// Handles the invoice detail query request
    /// </summary>
    /// <param name="request">The query containing the invoice ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed invoice information if successful</returns>
    public async Task<Result<InvoiceDetailDto>> Handle(InvoiceDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving invoice details for ID: {Id}", request.Id);

        var result = new Result<InvoiceDetailDto>();

        try
        {
            // Retrieve invoice with all related details from the repository
            var invoice = await _invoiceRepository.GetByIdAsync(request.Id);

            // If invoice not found, return a not found result
            if (invoice == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Rechnung mit ID {request.Id} wurde nicht gefunden");

                _logger.LogWarning("Invoice with ID {Id} not found", request.Id);
                return result;
            }

            // Get customer data to include customer name
            var customer = await _customerRepository.GetByIdAsync(invoice.CustomerId);
            var customerName = customer != null
                ? $"{customer.Firstname} {customer.Lastname}".Trim()
                : string.Empty;

            // Manual mapping from entity to DTO
            var data = new InvoiceDetailDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                CreatedDate = invoice.DateCreated,
                LastModifiedDate = invoice.DateModified,
                CustomerId = invoice.CustomerId,
                CustomerName = customerName,
                OrderId = invoice.OrderId,
                OrderNumber = invoice.OrderId.HasValue ? invoice.OrderId.Value.ToString() : string.Empty,
                InvoiceItems = invoice.InvoiceItems?.Select(item => new InvoiceItemDto
                {
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
                    ProductId = item.ProductId,
                    Name = item.Name,
                    //Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    //Tax = item.TaxRate,
                    //Subtotal = item.Subtotal,
                    //Total = item.Total
                }).ToList() ?? new List<InvoiceItemDto>(),
                Subtotal = invoice.Subtotal,
                ShippingCost = invoice.ShippingCost,
                TotalTax = invoice.TotalTax,
                Total = invoice.Total,
                PaymentStatus = invoice.PaymentStatus,
                InvoiceStatus = invoice.InvoiceStatus,
                PaymentMethod = invoice.PaymentMethod,
                PaymentTransactionId = invoice.PaymentTransactionId,
                Notes = invoice.Notes,
                // Invoice address details
                InvoiceAddressFirstName = invoice.InvoiceAddressFirstName,
                InvoiceAddressLastName = invoice.InvoiceAddressLastName,
                InvoiceAddressCompanyName = invoice.InvoiceAddressCompanyName,
                InvoiceAddressPhone = invoice.InvoiceAddressPhone,
                InvoiceAddressStreet = invoice.InvoiceAddressStreet,
                InvoiceAddressCity = invoice.InvoiceAddressCity,
                InvoiceAddressZip = invoice.InvoiceAddressZip,
                InvoiceAddressCountry = invoice.InvoiceAddressCountry,
                // Delivery address details
                DeliveryAddressFirstName = invoice.DeliveryAddressFirstName,
                DeliveryAddressLastName = invoice.DeliveryAddressLastName,
                DeliveryAddressCompanyName = invoice.DeliveryAddressCompanyName,
                DeliveryAddressPhone = invoice.DeliveryAddressPhone,
                DeliveryAddressStreet = invoice.DeliveryAddressStreet,
                DeliveryAddressCity = invoice.DeliveryAddressCity,
                DeliveryAddressZip = invoice.DeliveryAddressZip,
                DeliveryAddressCountry = invoice.DeliveryAddressCountry
            };

            // Set successful result with the invoice details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("Invoice with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during invoice retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"Ein Fehler ist beim Abrufen der Rechnungsdetails aufgetreten: {ex.Message}");

            _logger.LogError("Error retrieving invoice details: {Message}", ex.Message);
        }

        return result;
    }
}
