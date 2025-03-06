using FluentValidation;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class AiPromptInputValidator : AiPromptBaseValidator<AiPromptInputDto>
{
    /*public AiModelUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AiPromptInputDto>.CreateWithOptions(
            (AiPromptInputDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}