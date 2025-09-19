using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerDelete;

public class ManufacturerDeleteValidator : AbstractValidator<ManufacturerDeleteCommand>
{
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IProductRepository _productRepository;

    public ManufacturerDeleteValidator(
        IManufacturerRepository manufacturerRepository,
        IProductRepository productRepository)
    {
        _manufacturerRepository = manufacturerRepository;
        _productRepository = productRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");


        RuleFor(m => m)
            .MustAsync(ManufacturerIsNotUsedInProducts)
            .WithMessage("Cannot delete manufacturer as it is being used by one or more products.");
    }


    private async Task<bool> ManufacturerIsNotUsedInProducts(ManufacturerDeleteCommand command, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        return !products.Any(p => p.ManufacturerId == command.Id);
    }
}