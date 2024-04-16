using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }

    private async Task<bool> UserUnique(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique User name validation
        await Task.CompletedTask;
        return true;
    }
}