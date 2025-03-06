using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.AiModel.Commands.AiModelUpdate;

public class AiModelUpdateValidator : AiModelBaseValidator<AiModelInputCommand>
{
    private readonly IAiModelRepository _aiModelRepository;
    
    public AiModelUpdateValidator(IAiModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository;

        RuleFor(w => w)
            .MustAsync(AiModelExists).WithMessage("AiModel not found")
            .MustAsync(IsUniqueAsync).WithMessage("AiModel with the same name already exists.");
    }
    
    private async Task<bool> AiModelExists(AiModelInputCommand command, CancellationToken cancellationToken)
    {
        return await _aiModelRepository.GetByIdAsync(command.Id, true) != null;
    }
    
    private async Task<bool> IsUniqueAsync(AiModelInputCommand command, CancellationToken cancellationToken)
    {
        var aiModel = new Domain.Entities.AiModel
        {
            Name = command.Name,
        };

        return await _aiModelRepository.IsUniqueAsync(aiModel, command.Id);
    }
}