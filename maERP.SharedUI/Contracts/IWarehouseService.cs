using maERP.SharedUI.Models.Warehouse;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IWarehouseService
{
    Task<List<WarehouseVM>> GetWarehouses();
    Task<WarehouseVM> GetWarehouseDetails(int id);
    Task<Response<Guid>> CreateWarehouse(WarehouseVM warehouse);
    Task<Response<Guid>> UpdateWarehouse(int id, WarehouseVM warehouse);
    Task<Response<Guid>> DeleteWarehouse(int id);
}
