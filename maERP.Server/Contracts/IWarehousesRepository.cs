using maERP.Server.Data;
using maERP.Server.Models.Warehouse;

namespace maERP.Server.Contracts
{
    public interface IWarehousesRepository : IGenericRepository<Warehouse>
    {
        Task<WarehouseDto> GetDetails(int id);
    }
}