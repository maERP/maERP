using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteValidator : AbstractValidator<WarehouseDeleteCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public WarehouseDeleteValidator(
        IWarehouseRepository warehouseRepository,
        ISalesChannelRepository salesChannelRepository)
    {
        _warehouseRepository = warehouseRepository;
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(w => w)
            .MustAsync(WarehouseExists).WithMessage("Warehouse not found");

        RuleFor(w => w)
            .MustAsync(WarehouseIsNotUsedInSalesChannel)
            .WithMessage("Cannot delete warehouse as it is being used by one or more sales channels.");
    }

    private async Task<bool> WarehouseExists(WarehouseDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.ExistsAsync(command.Id);
    }

    private async Task<bool> WarehouseIsNotUsedInSalesChannel(WarehouseDeleteCommand command, CancellationToken cancellationToken)
    {
        var salesChannels = await _salesChannelRepository.GetAllAsync();
        return !salesChannels.Any(sc => sc.Warehouses != null && sc.Warehouses.Any(w => w.Id == command.Id));
    }
}
