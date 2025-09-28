using FluentValidation;

namespace maERP.Application.Features.Superadmin.Users.Commands.UserDelete;

/// <summary>
/// Validator for user deletion commands.
/// Implements FluentValidation's AbstractValidator to validate UserDeleteCommand properties.
/// </summary>
public class UserDeleteValidator : AbstractValidator<UserDeleteCommand>
{
    /// <summary>
    /// Constructor that initializes validation rules for user deletion
    /// </summary>
    public UserDeleteValidator()
    {
        // ID validation rules
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
