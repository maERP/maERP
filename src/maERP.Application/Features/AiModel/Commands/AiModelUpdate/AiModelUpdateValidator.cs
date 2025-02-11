using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AiModel.Commands.AiModelUpdate;

public class AiModelUpdateValidator : AbstractValidator<AiModelUpdateCommand>
{
    private readonly IAiModelRepository _aiModelRepository;
    
    public AiModelUpdateValidator(IAiModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository;
        
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.Name)
            .NotNull()
            .MinimumLength(3).WithMessage("{PropertyName} must be longer than 3.")
            .MaximumLength(255).WithMessage("{PropertyName} too long");

        RuleFor(w => w)
            .MustAsync(AiModelExists).WithMessage("AiModel not found")
            .MustAsync(IsUniqueAsync).WithMessage("AiModel with the same name already exists.");
    }
    
    private async Task<bool> AiModelExists(AiModelUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _aiModelRepository.GetByIdAsync(command.Id, true) != null;
    }
    
    private async Task<bool> IsUniqueAsync(AiModelUpdateCommand command, CancellationToken cancellationToken)
    {
        var aiModel = new Domain.Entities.AiModel
        {
            Name = command.Name,
        };

        return await _aiModelRepository.IsUniqueAsync(aiModel, command.Id);
    }
}