using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateValidator : AiPromptBaseValidator<AiPromptInputCommand>
{
    private readonly IAiPromptRepository _aIPromptRepository;
    
    public AiPromptUpdateValidator(IAiPromptRepository aIPromptRepository)
    {
        _aIPromptRepository = aIPromptRepository;

        RuleFor(w => w)
            .MustAsync(AiPromptExists).WithMessage("AiPrompt not found")
            .MustAsync(IsUniqueAsync).WithMessage("AiPrompt with the same name already exists.");
    }
    
    private async Task<bool> AiPromptExists(AiPromptInputCommand command, CancellationToken cancellationToken)
    {
        return await _aIPromptRepository.GetByIdAsync(command.Id, true) != null;
    }
    
    private async Task<bool> IsUniqueAsync(AiPromptInputCommand command, CancellationToken cancellationToken)
    {
        var aIPrompt = new Domain.Entities.AiPrompt
        {
            Id = command.Id,
            Identifier = command.Identifier,
        };
        return await _aIPromptRepository.IsUniqueAsync(aIPrompt, command.Id);
    }
}