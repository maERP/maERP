using FluentValidation;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class OrderCreateValidator : OrderBaseValidator<OrderCreateDto>
{
    /*public OrderCreateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<OrderCreateDto>.CreateWithOptions(
            (OrderCreateDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}