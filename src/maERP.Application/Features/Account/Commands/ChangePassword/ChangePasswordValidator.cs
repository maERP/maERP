using FluentValidation;

namespace maERP.Application.Features.Account.Commands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(p => p.CurrentPassword)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.NewPassword)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters long.");

        RuleFor(p => p.NewPasswordConfirm)
            .Equal(p => p.NewPassword).WithMessage("New password confirmation does not match.");

        RuleFor(p => p.NewPassword)
            .NotEqual(p => p.CurrentPassword).WithMessage("New password must differ from the current password.");
    }
}
