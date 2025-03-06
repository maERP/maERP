using FluentValidation;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class TaxClassInputValidator : TaxClassBaseValidator<TaxClassInputDto>
{
    /*public TaxClassUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TaxClassInputDto>.CreateWithOptions(
            (TaxClassInputDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}