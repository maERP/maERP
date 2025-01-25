using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.Warehouse;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IWarehouseService
{
    Task<PaginatedResult<WarehouseVm>> GetWarehouses(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<WarehouseVm> GetWarehouseDetails(int id);
    Task<Response<Guid>> CreateWarehouse(WarehouseVm warehouse);
    Task<Response<Guid>> UpdateWarehouse(int id, WarehouseVm warehouse);
    Task<Response<Guid>> DeleteWarehouse(int id);
}
