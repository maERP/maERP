using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IWarehousesRepository : IGenericRepository<Warehouse>
{
    Task<WarehouseDto> GetDetails(int id);
}