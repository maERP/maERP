using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerUpdate;

public class ManufacturerUpdateValidator : ManufacturerBaseValidator<ManufacturerUpdateCommand>
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public ManufacturerUpdateValidator(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;

        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(m => m)
            .MustAsync(ManufacturerExists).WithMessage("Manufacturer not found")
            .MustAsync(IsUniqueAsync).WithMessage("Manufacturer with the same name already exists.");
    }

    private async Task<bool> ManufacturerExists(ManufacturerUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _manufacturerRepository.GetByIdAsync(command.Id, true) != null;
    }

    private async Task<bool> IsUniqueAsync(ManufacturerUpdateCommand command, CancellationToken cancellationToken)
    {
        var manufacturer = new Domain.Entities.Manufacturer
        {
            Name = command.Name,
        };

        return await _manufacturerRepository.IsUniqueAsync(manufacturer, command.Id);
    }
}