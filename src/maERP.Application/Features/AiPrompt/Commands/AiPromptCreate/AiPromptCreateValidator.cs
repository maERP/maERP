using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateValidator : AbstractValidator<AiPromptCreateCommand>
{
    private readonly IAiPromptRepository _aIPromptRepository;
    
    public AiPromptCreateValidator(IAiPromptRepository aiPromptRepository)
    {
        _aIPromptRepository = aiPromptRepository;
        
        RuleFor(p => p.Identifier)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("AiPrompt with the same name already exists.");
    }

    private async Task<bool> IsUniqueAsync(AiPromptCreateCommand command, CancellationToken cancellationToken)
    {
        var aiPrompt = new Domain.Entities.AiPrompt()
        {
            Identifier = command.Identifier,
            PromptText = command.PromptText
        };
        
        return await _aIPromptRepository.IsUniqueAsync(aiPrompt);
    }
}