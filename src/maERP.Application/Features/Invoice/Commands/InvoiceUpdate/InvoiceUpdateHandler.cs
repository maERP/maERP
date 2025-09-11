using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
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

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceUpdateHandler(
        IAppLogger<InvoiceUpdateHandler> logger,
        IInvoiceRepository invoiceRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
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
        var validator = new InvoiceUpdateValidator(_invoiceRepository);
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
            // Manual mapping instead of using AutoMapper
            var invoiceToUpdate = new Domain.Entities.Invoice
            {
                Id = request.Id,
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

            // Update the invoice in the database
            await _invoiceRepository.UpdateAsync(invoiceToUpdate);

            // Set successful result with the updated invoice ID
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
