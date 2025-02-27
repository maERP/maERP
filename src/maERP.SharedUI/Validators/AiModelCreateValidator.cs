using FluentValidation;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class AiModelCreateValidator : AiModelBaseValidator<AiModelCreateDto>
{
    /*public AiModelCreateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AiModelCreateDto>.CreateWithOptions(
            (AiModelCreateDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}