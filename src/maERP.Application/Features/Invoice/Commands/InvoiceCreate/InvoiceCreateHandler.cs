using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Invoice.Commands.InvoiceCreate;

/// <summary>
/// Handler for processing invoice creation commands.
/// Implements IRequestHandler from MediatR to handle InvoiceCreateCommand requests
/// and return the ID of the newly created invoice wrapped in a Result.
/// </summary>
public class InvoiceCreateHandler : IRequestHandler<InvoiceCreateCommand, Result<int>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<InvoiceCreateHandler> _logger;

    /// <summary>
    /// Repository for invoice data operations
    /// </summary>
    private readonly IInvoiceRepository _invoiceRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceCreateHandler(
        IAppLogger<InvoiceCreateHandler> logger,
        IInvoiceRepository invoiceRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
    }

    /// <summary>
    /// Handles the invoice creation request
    /// </summary>
    /// <param name="request">The invoice creation command with invoice details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created invoice if successful</returns>
    public async Task<Result<int>> Handle(InvoiceCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new invoice with number: {InvoiceNumber}", request.InvoiceNumber);

        var result = new Result<int>();

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
                DeliveryAddressCountry = request.DeliveryAddressCountry
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
