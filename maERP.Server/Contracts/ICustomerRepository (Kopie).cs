using maERP.Shared.Models;
using maERP.Shared.Dtos.Order;

namespace maERP.Server.Contracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<OrderDto> GetDetails(int id);
    }
}