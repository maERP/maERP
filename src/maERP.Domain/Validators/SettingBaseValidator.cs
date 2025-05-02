using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class SettingBaseValidator<T> : AbstractValidator<T> where T : ISettingInputModel
{
    public SettingBaseValidator()
    {
        RuleFor(p => p.Key)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}