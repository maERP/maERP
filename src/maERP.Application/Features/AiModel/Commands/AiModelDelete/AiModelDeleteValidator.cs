using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AiModel.Commands.AiModelDelete;

public class AiModelDeleteValidator : AbstractValidator<AiModelDeleteCommand>
{
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelDeleteValidator(IAiModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository;
        
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(w => w)
            .MustAsync(AiModelExists).WithMessage("AiModel not found");
    }
    
    private async Task<bool> AiModelExists(AiModelDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _aiModelRepository.ExistsAsync(command.Id);
    }
}