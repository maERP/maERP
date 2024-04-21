using FluentValidation;

namespace maERP.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
        
        RuleFor(u => u)
            .MustAsync(UserUnique).WithMessage("User with the same email already exists.");
    }

    private async Task<bool> UserUnique(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique User name validation
        await Task.CompletedTask;
        return false;
    }
}