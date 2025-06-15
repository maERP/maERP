using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Invoice.Commands.InvoiceCreate;

/// <summary>
/// Validator for invoice creation commands.
/// Extends InvoiceBaseValidator to inherit common validation rules for invoice data
/// and adds specific validation for invoice creation operations.
/// </summary>
public class InvoiceCreateValidator : InvoiceBaseValidator<InvoiceCreateCommand>
{
    /// <summary>
    /// Repository for invoice data operations
    /// </summary>
    private readonly IInvoiceRepository _invoiceRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceCreateValidator(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;

        // Add validation rule for unique invoice numbers
        RuleFor(q => q.InvoiceNumber)
            .MustAsync(InvoiceNumberUniqueAsync).WithMessage("Eine Rechnung mit dieser Nummer existiert bereits.");
    }

    /// <summary>
    /// Asynchronously checks if an invoice with the same invoice number already exists in the database.
    /// </summary>
    /// <param name="invoiceNumber">The invoice number to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the invoice number is unique, false otherwise</returns>
    private async Task<bool> InvoiceNumberUniqueAsync(string invoiceNumber, CancellationToken cancellationToken)
    {
        // Check if an invoice with the same invoice number exists
        var existingInvoices = await _invoiceRepository.GetAllAsync();
        return !existingInvoices.Any(i => i.InvoiceNumber == invoiceNumber);
    }
}
