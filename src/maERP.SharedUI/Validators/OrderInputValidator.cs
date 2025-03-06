using FluentValidation;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class OrderInputValidator : OrderBaseValidator<OrderInputDto>
{
    /*public OrderUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<OrderInputDto>.CreateWithOptions(
            (OrderInputDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}