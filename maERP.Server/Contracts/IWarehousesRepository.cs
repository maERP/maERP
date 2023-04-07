using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IWarehousesRepository : IGenericRepository<Warehouse>
{
    Task<WarehouseDetailDto> GetDetails(int id);
}