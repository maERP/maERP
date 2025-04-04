using FluentValidation;

namespace maERP.Application.Features.User.Commands.UserCreate;

/// <summary>
/// Validator for user creation commands.
/// Implements FluentValidation's AbstractValidator to validate UserCreateCommand properties.
/// </summary>
public class UserCreateValidator : AbstractValidator<UserCreateCommand>
{
    /// <summary>
    /// Constructor that initializes validation rules for user creation
    /// </summary>
    public UserCreateValidator()
    {
        // Email validation rules
        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
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
        // TODO: Implement unique User name validation
        // Currently returns false as a placeholder until implementation is complete
        await Task.CompletedTask;
        return false;
    }
}