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
            .MustAsync(IsUniqueAsync).WithMessage("AiPrompt with the same name already exists.");
    }

    private async Task<bool> IsUniqueAsync(AiPromptCreateCommand command, CancellationToken cancellationToken)
    {
        // var aiModelContext = _aIPromptRepository.GetContext<Domain.Entities.AiModel>();
        
        var aiPrompt = new Domain.Entities.AiPrompt
        {
            // AiModel = aiModelContext.FirstOrDefault(x => x.Id == command.AiModel.Id) ?? null!,
            AiModelId = command.AiModelId,
            Identifier = command.Identifier,
            PromptText = command.PromptText
        };
        
        return await _aIPromptRepository.IsUniqueAsync(aiPrompt);
    }
}