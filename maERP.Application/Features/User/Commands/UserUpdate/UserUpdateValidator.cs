using FluentValidation;

namespace maERP.Application.Features.User.Commands.UserUpdate;

public class UserUpdateValidator : AbstractValidator<UserUpdateCommand>
{
    public UserUpdateValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}