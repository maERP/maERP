using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptDelete;

public class AIPromptDeleteValidator : AbstractValidator<AIPromptDeleteCommand>
{
    private readonly IAIPromptRepository _aIPromptRepository;

    public AIPromptDeleteValidator(IAIPromptRepository aIPromptRepository)
    {
        _aIPromptRepository = aIPromptRepository;
        
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(w => w)
            .MustAsync(AIPromptExists).WithMessage("AIPrompt not found");
        
        // TODO: Implement check if warehouse is not used in a sales channel
    }
    
    private async Task<bool> AIPromptExists(AIPromptDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _aIPromptRepository.ExistsAsync(command.Id);
    }
}