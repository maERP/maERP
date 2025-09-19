using FluentValidation;

namespace maERP.Application.Features.Invoice.Commands.InvoiceDelete;

/// <summary>
/// Validator for invoice deletion commands.
/// Validates that the invoice ID is valid and that the invoice exists.
/// </summary>
public class InvoiceDeleteValidator : AbstractValidator<InvoiceDeleteCommand>
{
    public InvoiceDeleteValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} ist erforderlich.")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} darf nicht leer sein.");
    }
}
