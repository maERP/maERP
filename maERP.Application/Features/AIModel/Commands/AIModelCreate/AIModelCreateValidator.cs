using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AIModel.Commands.AIModelCreate;

public class AIModelCreateValidator : AbstractValidator<AIModelCreateCommand>
{
    private readonly IAIModelRepository _aiModelRepository;
    
    public AIModelCreateValidator(IAIModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository;
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("AIModel with the same name already exists.");
    }

    private async Task<bool> IsUniqueAsync(AIModelCreateCommand command, CancellationToken cancellationToken)
    {
        var aiModel = new Domain.Entities.AIModel()
        {
            Name = command.Name,
        };
        
        return await _aiModelRepository.IsUniqueAsync(aiModel);
    }
}