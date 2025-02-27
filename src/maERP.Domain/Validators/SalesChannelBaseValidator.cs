using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class SalesChannelBaseValidator<T> : AbstractValidator<T> where T : ISalesChannelInputModel
{
    public SalesChannelBaseValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}