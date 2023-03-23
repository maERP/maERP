using maERP.Shared.Dtos.Order;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IOrdersRepository : IGenericRepository<Order>
{
    Task<OrderDto> GetDetails(int id);
}