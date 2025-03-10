using FluentValidation;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class SalesChannelInputValidator : SalesChannelBaseValidator<SalesChannelInputDto>
{
    /*public SalesChannelUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<SalesChannelInputDto>.CreateWithOptions(
            (SalesChannelInputDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}