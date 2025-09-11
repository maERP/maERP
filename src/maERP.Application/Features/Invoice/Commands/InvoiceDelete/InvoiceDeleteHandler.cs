using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
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

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceDeleteHandler(
        IAppLogger<InvoiceDeleteHandler> logger,
        IInvoiceRepository invoiceRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
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
        var validator = new InvoiceDeleteValidator(_invoiceRepository);
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
            // Create entity to delete
            var invoiceToDelete = new Domain.Entities.Invoice
            {
                Id = request.Id
            };

            // Delete from database
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
