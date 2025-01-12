using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteValidator : AbstractValidator<AiPromptDeleteCommand>
{
    private readonly IAiPromptRepository _aIPromptRepository;

    public AiPromptDeleteValidator(IAiPromptRepository aIPromptRepository)
    {
        _aIPromptRepository = aIPromptRepository;
        
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(w => w)
            .MustAsync(AiPromptExists).WithMessage("AiPrompt not found");
        
        // TODO: Implement check if warehouse is not used in a sales channel
    }
    
    private async Task<bool> AiPromptExists(AiPromptDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _aIPromptRepository.ExistsAsync(command.Id);
    }
}