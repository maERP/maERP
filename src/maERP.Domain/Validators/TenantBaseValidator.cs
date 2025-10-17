using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class TenantBaseValidator<T> : AbstractValidator<T> where T : ITenantInputModel
{
    public TenantBaseValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(p => p.ContactEmail)
            .EmailAddress().WithMessage("{PropertyName} must be a valid email address.")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.")
            .When(p => !string.IsNullOrEmpty(p.ContactEmail));
    }
}