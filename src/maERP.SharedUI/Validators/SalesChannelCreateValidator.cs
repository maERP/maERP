using FluentValidation;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class SalesChannelCreateValidator : SalesChannelBaseValidator<SalesChannelCreateDto>
{
    /*public SalesChannelCreateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<SalesChannelCreateDto>.CreateWithOptions(
            (SalesChannelCreateDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}