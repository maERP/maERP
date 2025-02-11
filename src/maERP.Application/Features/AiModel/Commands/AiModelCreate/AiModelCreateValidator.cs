using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

public class AiModelCreateValidator : AbstractValidator<AiModelCreateCommand>
{
    private readonly IAiModelRepository _aiModelRepository;
    
    public AiModelCreateValidator(IAiModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository;
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

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