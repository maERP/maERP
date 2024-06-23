using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AIModel.Commands.AIModelUpdate;

public class AIModelUpdateValidator : AbstractValidator<AIModelUpdateCommand>
{
    private readonly IAIModelRepository _aimodelRepository;
    
    public AIModelUpdateValidator(IAIModelRepository aimodelRepository)
    {
        _aimodelRepository = aimodelRepository;
        
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.Name)
            .NotNull()
            .MinimumLength(3).WithMessage("{PropertyName} must be longer than 3.")
            .MaximumLength(255).WithMessage("{PropertyName} too long");

        RuleFor(w => w)
            .MustAsync(AIModelExists).WithMessage("AIModel not found")
            .MustAsync(IsUniqueAsync).WithMessage("AIModel with the same name already exists.");
    }
    
    private async Task<bool> AIModelExists(AIModelUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _aimodelRepository.GetByIdAsync(command.Id, true) != null;
    }
    
    private async Task<bool> IsUniqueAsync(AIModelUpdateCommand command, CancellationToken cancellationToken)
    {
        var aimodel = new Domain.Entities.AIModel()
        {
            Name = command.Name,
        };

        return await _aimodelRepository.IsUniqueAsync(aimodel, command.Id);
    }
}