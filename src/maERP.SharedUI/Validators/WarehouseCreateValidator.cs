using FluentValidation;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Validators;

namespace maERP.SharedUI.Validators;

public class WarehouseCreateValidator : WarehouseBaseValidator<WarehouseCreateDto>
{
    /*public WarehouseCreateValidator()
    {
    }*/
    
    // Client-specific validation method
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<WarehouseCreateDto>.CreateWithOptions(
            (WarehouseCreateDto)model, 
            x => x.IncludeProperties(propertyName)));
        
        return result.IsValid ? 
            Array.Empty<string>() : 
            result.Errors.Select(e => e.ErrorMessage);
    };
}