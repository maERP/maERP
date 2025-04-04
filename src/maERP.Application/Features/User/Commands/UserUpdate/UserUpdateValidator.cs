using FluentValidation;

namespace maERP.Application.Features.User.Commands.UserUpdate;

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
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}