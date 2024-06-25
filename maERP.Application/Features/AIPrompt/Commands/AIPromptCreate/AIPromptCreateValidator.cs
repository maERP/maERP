using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptCreate;

public class AIPromptCreateValidator : AbstractValidator<AIPromptCreateCommand>
{
    private readonly IAIPromptRepository _aIPromptRepository;
    
    public AIPromptCreateValidator(IAIPromptRepository aiPromptRepository)
    {
        _aIPromptRepository = aiPromptRepository;
        
        RuleFor(p => p.Identifier)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("AIPrompt with the same name already exists.");
    }

    private async Task<bool> IsUniqueAsync(AIPromptCreateCommand command, CancellationToken cancellationToken)
    {
        var aiPrompt = new Domain.Entities.AIPrompt()
        {
            Identifier = command.Identifier,
            PromptText = command.PromptText
        };
        
        return await _aIPromptRepository.IsUniqueAsync(aiPrompt);
    }
}