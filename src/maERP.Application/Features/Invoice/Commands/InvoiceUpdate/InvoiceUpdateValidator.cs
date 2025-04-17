using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Invoice.Commands.InvoiceUpdate;

/// <summary>
/// Validator for invoice update commands.
/// Extends InvoiceBaseValidator to inherit common validation rules for invoice data
/// and adds specific validation for invoice update operations.
/// </summary>
public class InvoiceUpdateValidator : InvoiceBaseValidator<InvoiceInputCommand>
{
    private readonly IInvoiceRepository _invoiceRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    public InvoiceUpdateValidator(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
        
        // Add validation rules specific to update operations
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als 0 sein.");

        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} ist erforderlich.")
            .NotEmpty().WithMessage("{PropertyName} ist erforderlich.");

        RuleFor(i => i)
            .MustAsync(InvoiceExists).WithMessage("Rechnung wurde nicht gefunden");
            
        RuleFor(i => i)
            .MustAsync(InvoiceNumberIsUniqueOrUnchanged).WithMessage("Eine Rechnung mit dieser Nummer existiert bereits");
    }
    
    /// <summary>
    /// Asynchronously checks if an invoice with the given ID exists in the database.
    /// </summary>
    /// <param name="command">The invoice update command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the invoice exists, false otherwise</returns>
    private async Task<bool> InvoiceExists(InvoiceInputCommand command, CancellationToken cancellationToken)
    {
        return await _invoiceRepository.ExistsAsync(command.Id);
    }
    
    /// <summary>
    /// Asynchronously checks if the invoice number is unique or unchanged.
    /// </summary>
    /// <param name="command">The invoice update command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the invoice number is unique or unchanged, false otherwise</returns>
    private async Task<bool> InvoiceNumberIsUniqueOrUnchanged(InvoiceInputCommand command, CancellationToken cancellationToken)
    {
        var existingInvoices = await _invoiceRepository.GetAllAsync();
        var currentInvoice = await _invoiceRepository.GetByIdAsync(command.Id);
        
        // If it's the same invoice number as before, that's fine
        if (currentInvoice != null && currentInvoice.InvoiceNumber == command.InvoiceNumber)
        {
            return true;
        }
        
        // Otherwise, check if the new invoice number is unique
        return !existingInvoices.Any(i => i.InvoiceNumber == command.InvoiceNumber);
    }
}
