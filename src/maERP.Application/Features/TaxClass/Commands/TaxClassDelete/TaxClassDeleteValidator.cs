using FluentValidation;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteValidator : AbstractValidator<TaxClassDeleteCommand>
{
    public TaxClassDeleteValidator()
    {
        RuleFor(p => p.Id)
            .NotNull();
    }
}