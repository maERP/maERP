using maERP.Client.Features.Warehouses.Services;
using maERP.Domain.Dtos.Warehouse;

namespace maERP.Client.Features.Warehouses.Models;

/// <summary>
/// Model for warehouse detail page using MVUX pattern.
/// Receives warehouse ID from navigation data.
/// </summary>
public partial record WarehouseDetailModel
{
    private readonly IWarehouseService _warehouseService;
    private readonly INavigator _navigator;
    private readonly Guid _warehouseId;

    public WarehouseDetailModel(
        IWarehouseService warehouseService,
        INavigator navigator,
        WarehouseDetailData data)
    {
        _warehouseService = warehouseService;
        _navigator = navigator;
        _warehouseId = data.WarehouseId;
    }

    /// <summary>
    /// Feed that loads the warehouse details.
    /// </summary>
    public IFeed<WarehouseDetailDto> Warehouse => Feed.Async(async ct =>
    {
        var warehouse = await _warehouseService.GetWarehouseAsync(_warehouseId, ct);
        return warehouse ?? throw new InvalidOperationException($"Warehouse {_warehouseId} not found");
    });

    /// <summary>
    /// Navigate back to warehouse list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Navigate to edit warehouse page.
    /// </summary>
    public async Task EditWarehouse()
    {
        await _navigator.NavigateDataAsync(this, new WarehouseEditData(_warehouseId));
    }
}
