using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Invoice.Commands.InvoiceDelete;

/// <summary>
/// Validator for invoice deletion commands.
/// Validates that the invoice ID is valid and that the invoice exists.
/// </summary>
public class InvoiceDeleteValidator : AbstractValidator<InvoiceDeleteCommand>
{
    private readonly IInvoiceRepository _invoiceRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceDeleteValidator(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als 0 sein.");

        RuleFor(i => i)
            .MustAsync(InvoiceExists).WithMessage("Rechnung wurde nicht gefunden");
    }

    /// <summary>
    /// Asynchronously checks if an invoice with the given ID exists in the database.
    /// </summary>
    /// <param name="command">The invoice delete command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the invoice exists, false otherwise</returns>
    private async Task<bool> InvoiceExists(InvoiceDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _invoiceRepository.ExistsAsync(command.Id);
    }
}
