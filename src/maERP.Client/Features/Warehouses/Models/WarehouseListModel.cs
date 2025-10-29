namespace maERP.Client.Features.Warehouses.Models;

public partial record WarehouseListModel
{
    private readonly IWarehousesApiClient _warehousesApiClient;

    public WarehouseListModel(IWarehousesApiClient warehousesApiClient)
    {
        _warehousesApiClient = warehousesApiClient;
    }
}
