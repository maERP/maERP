using FluentValidation;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class ProductUpdateValidator : ProductBaseValidator<ProductUpdateDto>
{
    /*public ProductUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ProductUpdateDto>.CreateWithOptions(
            (ProductUpdateDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}