using maERP.Data.Models;
using maERP.Data.Dtos.Warehouse;

namespace maERP.Server.Contracts
{
    public interface IWarehousesRepository : IGenericRepository<Warehouse>
    {
        Task<WarehouseDto> GetDetails(int id);
    }
}