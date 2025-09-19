using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Invoice.Commands.InvoiceDelete;

/// <summary>
/// Handler for processing invoice deletion commands.
/// Implements IRequestHandler from MediatR to handle DeleteInvoiceCommand requests
/// and return the ID of the deleted invoice wrapped in a Result.
/// </summary>
public class InvoiceDeleteHandler : IRequestHandler<InvoiceDeleteCommand, Result<Guid>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<InvoiceDeleteHandler> _logger;

    /// <summary>
    /// Repository for invoice data operations
    /// </summary>
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ITenantContext _tenantContext;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceDeleteHandler(
        IAppLogger<InvoiceDeleteHandler> logger,
        IInvoiceRepository invoiceRepository,
        ITenantContext tenantContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    /// <summary>
    /// Handles the invoice deletion request
    /// </summary>
    /// <param name="request">The invoice deletion command with invoice ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the deleted invoice if successful</returns>
    public async Task<Result<Guid>> Handle(InvoiceDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting invoice with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new InvoiceDeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(InvoiceDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            var currentTenantId = _tenantContext.GetCurrentTenantId();
            if (!currentTenantId.HasValue || currentTenantId == Guid.Empty)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Rechnung wurde nicht gefunden.");
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

            var invoiceToDelete = await _invoiceRepository.GetInvoiceWithDetailsAsync(request.Id);
            if (invoiceToDelete == null || invoiceToDelete.TenantId != currentTenantId.Value)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Rechnung wurde nicht gefunden.");
                return result;
            }

            if (invoiceToDelete.PaymentStatus == PaymentStatus.CompletelyPaid)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Bezahlte Rechnungen können nicht gelöscht werden.");
                return result;
            }

            await _invoiceRepository.DeleteAsync(invoiceToDelete);

            // Set successful result with the deleted invoice ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = invoiceToDelete.Id;

            _logger.LogInformation("Successfully deleted invoice with ID: {Id}", invoiceToDelete.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during invoice deletion
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"Ein Fehler ist beim Löschen der Rechnung aufgetreten: {ex.Message}");

            _logger.LogError("Error deleting invoice: {Message}", ex.Message);
        }

        return result;
    }
}
