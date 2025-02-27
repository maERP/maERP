using FluentValidation;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class AiPromptUpdateValidator : AiPromptBaseValidator<AiPromptUpdateDto>
{
    /*public AiPromptUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AiPromptUpdateDto>.CreateWithOptions(
            (AiPromptUpdateDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}