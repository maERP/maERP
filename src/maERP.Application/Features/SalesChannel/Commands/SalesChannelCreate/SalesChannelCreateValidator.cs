using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

/// <summary>
/// Validator for sales channel creation commands.
/// Extends SalesChannelBaseValidator to inherit common validation rules for sales channel data
/// and adds specific validation for sales channel creation operations.
/// </summary>
public class SalesChannelCreateValidator : SalesChannelBaseValidator<SalesChannelCreateCommand>
{
    /// <summary>
    /// Repository for sales channel data operations
    /// </summary>
    private readonly ISalesChannelRepository _salesChannelRepository;

    /// <summary>
    /// Repository for warehouse data operations
    /// </summary>
    private readonly IWarehouseRepository _warehouseRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="salesChannelRepository">Repository for sales channel data access</param>
    /// <param name="warehouseRepository">Repository for warehouse data access</param>
    public SalesChannelCreateValidator(ISalesChannelRepository salesChannelRepository, IWarehouseRepository warehouseRepository)
    {
        _salesChannelRepository = salesChannelRepository;
        _warehouseRepository = warehouseRepository;

        // Add rule to check if the sales channel name is unique before creating
        RuleFor(s => s)
            .MustAsync(IsUniqueAsync).WithMessage("SalesChannel with the same name already exists.");

        // Add rule to check if all warehouse IDs are valid
        RuleFor(s => s.WarehouseIds)
            .MustAsync(AreWarehousesValidAsync).WithMessage("One or more warehouse IDs are invalid.");
    }

    /// <summary>
    /// Asynchronously checks if a sales channel with the same name already exists in the database
    /// </summary>
    /// <param name="command">The sales channel creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sales channel name is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(SalesChannelCreateCommand command, CancellationToken cancellationToken)
    {
        var salesChannel = new Domain.Entities.SalesChannel
        {
            Name = command.Name
        };

        return await _salesChannelRepository.SalesChannelIsUniqueAsync(salesChannel);
    }

    /// <summary>
    /// Asynchronously checks if all warehouse IDs in the list exist and belong to the current tenant
    /// </summary>
    /// <param name="warehouseIds">List of warehouse IDs to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if all warehouse IDs are valid, false otherwise</returns>
    private async Task<bool> AreWarehousesValidAsync(List<Guid> warehouseIds, CancellationToken cancellationToken)
    {
        if (warehouseIds == null || !warehouseIds.Any())
            return true; // Allow empty warehouse list

        // Check if all warehouse IDs exist in the database (tenant filtering is handled by repository)
        foreach (var warehouseId in warehouseIds)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (warehouse == null)
                return false;
        }
        return true;
    }
}