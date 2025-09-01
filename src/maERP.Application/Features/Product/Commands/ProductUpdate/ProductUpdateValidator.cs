using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateValidator : ProductBaseValidator<ProductUpdateCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ITaxClassRepository _taxClassRepository;
    private readonly IManufacturerRepository _manufacturerRepository;

    public ProductUpdateValidator(
        IProductRepository productRepository,
        ITaxClassRepository taxClassRepository,
        IManufacturerRepository manufacturerRepository)
    {
        _productRepository = productRepository;
        _taxClassRepository = taxClassRepository;
        _manufacturerRepository = manufacturerRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p)
            .MustAsync(ProductExists).WithMessage("Product not found");

        // Add rule to check if the tax class exists
        RuleFor(p => p.TaxClassId)
            .MustAsync(TaxClassExists).WithMessage("Tax class does not exist.");

        // Add rule to check if the manufacturer exists (when not null)
        RuleFor(p => p.ManufacturerId)
            .MustAsync(ManufacturerExists).WithMessage("Manufacturer does not exist.")
            .When(p => p.ManufacturerId.HasValue);
    }

    private async Task<bool> ProductExists(ProductUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(command.Id, true) != null;
    }

    private async Task<bool> TaxClassExists(int taxClassId, CancellationToken cancellationToken)
    {
        return await _taxClassRepository.ExistsAsync(taxClassId);
    }

    private async Task<bool> ManufacturerExists(int? manufacturerId, CancellationToken cancellationToken)
    {
        if (!manufacturerId.HasValue)
            return true;

        return await _manufacturerRepository.ExistsAsync(manufacturerId.Value);
    }
}