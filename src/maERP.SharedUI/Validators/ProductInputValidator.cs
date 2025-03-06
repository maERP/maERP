using FluentValidation;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class ProductInputValidator : ProductBaseValidator<ProductInputDto>
{
    /*public ProductUpdateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ProductInputDto>.CreateWithOptions(
            (ProductInputDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}