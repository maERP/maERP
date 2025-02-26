using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

public class AiModelCreateValidator : AbstractValidator<AiModelCreateCommand>
{
    private readonly IAiModelRepository _aiModelRepository;
    
    public AiModelCreateValidator(IAiModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
        
        // Name validation
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            
        // AiModelType validation
        RuleFor(p => p.AiModelType)
            .IsInEnum().WithMessage("{PropertyName} must be a valid model type.")
            .NotEqual((int)AiModelType.None).WithMessage("{PropertyName} cannot be 'None'.");
            
        // API credentials validation - at least one authentication method must be provided
        RuleFor(p => p)
            .Must(cmd => !string.IsNullOrWhiteSpace(cmd.ApiKey) || 
                         (!string.IsNullOrWhiteSpace(cmd.ApiUsername) && !string.IsNullOrWhiteSpace(cmd.ApiPassword)))
            .WithMessage("Either ApiKey or both ApiUsername and ApiPassword must be provided.");
            
        // API Key validation (if provided)
        When(p => !string.IsNullOrWhiteSpace(p.ApiKey), () => {
            RuleFor(p => p.ApiKey)
                .MinimumLength(10).WithMessage("{PropertyName} must be at least 10 characters long.");
        });
        
        // Username/Password validation (if provided)
        When(p => !string.IsNullOrWhiteSpace(p.ApiUsername) || !string.IsNullOrWhiteSpace(p.ApiPassword), () => {
            RuleFor(p => p.ApiUsername)
                .NotEmpty().WithMessage("{PropertyName} is required when ApiPassword is provided.");
                
            RuleFor(p => p.ApiPassword)
                .NotEmpty().WithMessage("{PropertyName} is required when ApiUsername is provided.");
        });
            
        // Uniqueness validation
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("AiModel with the same name already exists.");
    }

    private async Task<bool> IsUniqueAsync(AiModelCreateCommand command, CancellationToken cancellationToken)
    {
        var aiModel = new Domain.Entities.AiModel
        {
            Name = command.Name,
        };
        
        return await _aiModelRepository.IsUniqueAsync(aiModel);
    }
}