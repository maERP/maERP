using FluentValidation;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class AiModelsInputValidator : AiModelBaseValidator<AiModelInputDto>
{
    /*public AiModelCreateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AiModelInputDto>.CreateWithOptions(
            (AiModelInputDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}