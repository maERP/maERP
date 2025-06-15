using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateValidator : AiPromptBaseValidator<AiPromptCreateCommand>
{
    private readonly IAiPromptRepository _aIPromptRepository;

    public AiPromptCreateValidator(IAiPromptRepository aiPromptRepository)
    {
        _aIPromptRepository = aiPromptRepository;

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("AiPrompt with the same Identifier already exists.");
    }

    private async Task<bool> IsUniqueAsync(AiPromptCreateCommand command, CancellationToken cancellationToken)
    {
        var aiPrompt = new Domain.Entities.AiPrompt
        {
            Identifier = command.Identifier
        };

        return await _aIPromptRepository.IsUniqueAsync(aiPrompt);
    }
}