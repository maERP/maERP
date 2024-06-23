using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AIModel.Commands.AIModelDelete;

public class AIModelDeleteValidator : AbstractValidator<AIModelDeleteCommand>
{
    private readonly IAIModelRepository _aiModelRepository;

    public AIModelDeleteValidator(IAIModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository;
        
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(w => w)
            .MustAsync(AIModelExists).WithMessage("AIModel not found");
        
        // TODO: Implement check if warehouse is not used in a sales channel
    }
    
    private async Task<bool> AIModelExists(AIModelDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _aiModelRepository.ExistsAsync(command.Id);
    }
}