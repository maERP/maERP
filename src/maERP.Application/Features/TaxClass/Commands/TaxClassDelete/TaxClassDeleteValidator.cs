using FluentValidation;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteValidator : AbstractValidator<TaxClassDeleteCommand>
{
    public TaxClassDeleteValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
    }
}