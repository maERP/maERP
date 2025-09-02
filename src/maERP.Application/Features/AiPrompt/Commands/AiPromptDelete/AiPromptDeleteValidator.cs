using FluentValidation;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteValidator : AbstractValidator<AiPromptDeleteCommand>
{
    public AiPromptDeleteValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        // Note: Existence check is performed in the handler to return proper 404 status
        // TODO: Implement check if AI prompt is not used in other entities
    }
}