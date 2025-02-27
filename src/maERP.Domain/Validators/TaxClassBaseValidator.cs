using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class TaxClassBaseValidator<T> : AbstractValidator<T> where T : ITaxClassInputModel
{
    public TaxClassBaseValidator()
    {
        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");
    }
}