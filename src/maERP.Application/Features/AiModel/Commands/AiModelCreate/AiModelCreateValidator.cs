using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

using FluentValidation;

public class AiModelCreateValidator : AiModelBaseValidator<AiModelCreateCommand>
{
    private readonly IAiModelRepository _aiModelRepository;
    
    public AiModelCreateValidator(IAiModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
            
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