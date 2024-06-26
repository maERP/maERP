using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateValidator : AbstractValidator<AiPromptUpdateCommand>
{
    private readonly IAiPromptRepository _aIPromptRepository;
    
    public AiPromptUpdateValidator(IAiPromptRepository aIPromptRepository)
    {
        _aIPromptRepository = aIPromptRepository;
        
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.Identifier)
            .NotNull()
            .MinimumLength(3).WithMessage("{PropertyName} must be longer than 3.")
            .MaximumLength(255).WithMessage("{PropertyName} too long");

        RuleFor(w => w)
            .MustAsync(AiPromptExists).WithMessage("AiPrompt not found")
            .MustAsync(IsUniqueAsync).WithMessage("AiPrompt with the same name already exists.");
    }
    
    private async Task<bool> AiPromptExists(AiPromptUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _aIPromptRepository.GetByIdAsync(command.Id, true) != null;
    }
    
    private async Task<bool> IsUniqueAsync(AiPromptUpdateCommand command, CancellationToken cancellationToken)
    {
        var aIPrompt = new Domain.Entities.AiPrompt()
        {
            Identifier = command.Identifier,
            PromptText = command.PromptText
        };

        return await _aIPromptRepository.IsUniqueAsync(aIPrompt, command.Id);
    }
}