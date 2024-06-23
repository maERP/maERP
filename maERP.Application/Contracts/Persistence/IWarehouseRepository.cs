using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    // bool IsUnique(string name);
}