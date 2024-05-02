using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public CreateWarehouseCommandValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("Warehouse with the same name already exists.");
    }

    private async Task<bool> IsUniqueAsync(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        var warehouse = new Domain.Models.Warehouse()
        {
            Name = command.Name,
        };
        
        return await _warehouseRepository.IsUniqueAsync(warehouse);
    }
}