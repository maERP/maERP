using maERP.Shared.Models;
using maERP.Shared.Dtos.Warehouse;

namespace maERP.Server.Contracts
{
    public interface IWarehousesRepository : IGenericRepository<Warehouse>
    {
        Task<WarehouseDto> GetDetails(int id);
    }
}