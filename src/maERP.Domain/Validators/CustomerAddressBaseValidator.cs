using FluentValidation;
using maERP.Domain.Dtos.CustomerAddress;

namespace maERP.Domain.Validators;

/// <summary>
/// Base validator for CustomerAddress - contains field-based validation rules.
/// Used by Client and Server validators.
/// </summary>
public class CustomerAddressBaseValidator : AbstractValidator<CustomerAddressListDto>
{
    public CustomerAddressBaseValidator()
    {
        RuleFor(a => a.CountryId)
            .NotEqual(Guid.Empty).WithMessage("CountryId is required for address.");

        RuleFor(a => a.Firstname)
            .NotEmpty().WithMessage("Firstname is required for address.")
            .MaximumLength(100).WithMessage("Firstname must not exceed 100 characters.");

        RuleFor(a => a.Lastname)
            .NotEmpty().WithMessage("Lastname is required for address.")
            .MaximumLength(100).WithMessage("Lastname must not exceed 100 characters.");

        RuleFor(a => a.Street)
            .NotEmpty().WithMessage("Street is required for address.")
            .MaximumLength(200).WithMessage("Street must not exceed 200 characters.");

        RuleFor(a => a.Zip)
            .NotEmpty().WithMessage("Zip is required for address.")
            .MaximumLength(20).WithMessage("Zip must not exceed 20 characters.");

        RuleFor(a => a.City)
            .NotEmpty().WithMessage("City is required for address.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");
    }
}
