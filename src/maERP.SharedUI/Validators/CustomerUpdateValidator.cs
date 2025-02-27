using FluentValidation;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class CustomerUpdateValidator : CustomerBaseValidator<CustomerUpdateDto>
{
    /*public CustomerUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CustomerUpdateDto>.CreateWithOptions(
            (CustomerUpdateDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}