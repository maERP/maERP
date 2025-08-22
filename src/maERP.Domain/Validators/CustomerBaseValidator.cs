using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class CustomerBaseValidator<T> : AbstractValidator<T> where T : ICustomerInputModel
{
    public CustomerBaseValidator()
    {
        RuleFor(p => p.Firstname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Lastname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
    }
}