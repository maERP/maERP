using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.User.Commands.UserCreate;

/// <summary>
/// Validator for user creation commands.
/// Implements FluentValidation's AbstractValidator to validate UserCreateCommand properties.
/// </summary>
public class UserCreateValidator : AbstractValidator<UserCreateCommand>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor that initializes validation rules for user creation
    /// </summary>
    public UserCreateValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        // Email validation rules
        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");

        RuleFor(p => p.Password)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.Firstname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must be 100 characters or fewer.");

        RuleFor(p => p.Lastname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must be 100 characters or fewer.");

        RuleFor(p => p.DefaultTenantId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        // Unique user validation rule
        RuleFor(u => u)
            .MustAsync(UserUnique).WithMessage("User with the same email already exists.");
    }

    /// <summary>
    /// Validates that the user email is unique in the system
    /// </summary>
    /// <param name="command">The user creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user email is unique, false otherwise</returns>
    private async Task<bool> UserUnique(UserCreateCommand command, CancellationToken cancellationToken)
    {
        return !await _userRepository.EmailExistsAsync(command.Email);
    }
}
