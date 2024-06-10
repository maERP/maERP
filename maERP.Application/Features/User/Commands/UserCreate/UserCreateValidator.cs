using FluentValidation;

namespace maERP.Application.Features.User.Commands.UserCreate;

public class UserCreateValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateValidator()
    {
        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
        
        RuleFor(u => u)
            .MustAsync(UserUnique).WithMessage("User with the same email already exists.");
    }

    private async Task<bool> UserUnique(UserCreateCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique User name validation
        await Task.CompletedTask;
        return false;
    }
}