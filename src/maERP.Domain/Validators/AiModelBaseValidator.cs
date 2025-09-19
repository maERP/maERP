using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class AiModelBaseValidator<T> : AbstractValidator<T> where T : IAiModelInputModel
{
    public AiModelBaseValidator()
    {
        // Name validation
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        // AiModelType validation
        RuleFor(p => p.AiModelType)
            .IsInEnum().WithMessage("{PropertyName} must be a valid model type.");

        // API credentials validation - at least one authentication method must be provided
        RuleFor(p => p)
            .Must(cmd => !string.IsNullOrWhiteSpace(cmd.ApiKey) ||
                         (!string.IsNullOrWhiteSpace(cmd.ApiUsername) && !string.IsNullOrWhiteSpace(cmd.ApiPassword)))
            .WithMessage("Either ApiKey or both ApiUsername and ApiPassword must be provided.");

        // API Key validation (if provided)
        When(p => !string.IsNullOrWhiteSpace(p.ApiKey), () =>
        {
            RuleFor(p => p.ApiKey)
                .MinimumLength(10).WithMessage("{PropertyName} must be at least 10 characters long.");
        });

        // Username/Password validation (if provided)
        When(p => !string.IsNullOrWhiteSpace(p.ApiUsername) || !string.IsNullOrWhiteSpace(p.ApiPassword), () =>
        {
            RuleFor(p => p.ApiUsername)
                .NotEmpty().WithMessage("{PropertyName} is required when ApiPassword is provided.");

            RuleFor(p => p.ApiPassword)
                .NotEmpty().WithMessage("{PropertyName} is required when ApiUsername is provided.");
        });
    }
}