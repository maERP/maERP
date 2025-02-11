using FluentValidation;

namespace maERP.Application.Features.User.Commands.UserUpdate;

public class UserUpdateValidator : AbstractValidator<UserUpdateCommand>
{
    public UserUpdateValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}