using FluentValidation;

namespace maERP.Application.Features.Superadmin.Users.Commands.UserUpdate;

/// <summary>
/// Validator for user update commands.
/// Implements FluentValidation's AbstractValidator to validate UserUpdateCommand properties.
/// </summary>
public class UserUpdateValidator : AbstractValidator<UserUpdateCommand>
{
    /// <summary>
    /// Constructor that initializes validation rules for user update
    /// </summary>
    public UserUpdateValidator()
    {
        // ID validation rules
        RuleFor(p => p.Id)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.");

        // Email validation rules
        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(256).WithMessage("{PropertyName} must be 256 characters or fewer.");

        RuleFor(p => p.Firstname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must be 100 characters or fewer.");

        RuleFor(p => p.Lastname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must be 100 characters or fewer.");

        RuleFor(p => p.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .When(p => !string.IsNullOrWhiteSpace(p.Password));
    }
}
