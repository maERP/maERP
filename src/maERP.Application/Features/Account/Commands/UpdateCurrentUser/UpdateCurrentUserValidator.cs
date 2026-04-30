using FluentValidation;

namespace maERP.Application.Features.Account.Commands.UpdateCurrentUser;

public class UpdateCurrentUserValidator : AbstractValidator<UpdateCurrentUserCommand>
{
    public UpdateCurrentUserValidator()
    {
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

        RuleFor(p => p.PhoneNumber)
            .MaximumLength(50).WithMessage("{PropertyName} must be 50 characters or fewer.");
    }
}
