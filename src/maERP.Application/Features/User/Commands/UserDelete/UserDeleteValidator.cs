using FluentValidation;

namespace maERP.Application.Features.User.Commands.UserDelete;

public class UserDeleteValidator : AbstractValidator<UserDeleteCommand>
{
    public UserDeleteValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
