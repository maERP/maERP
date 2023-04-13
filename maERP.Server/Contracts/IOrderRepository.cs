using maERP.Shared.Dtos.Order;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<OrderDetailDto> GetDetails(int id);
}