using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IOrderService
{
    Task<PaginatedResult<OrderListVm>> GetOrders(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<OrderVm> GetOrderDetails(int id);
    Task<Response<Guid>> CreateOrder(OrderVm order);
    Task<Response<Guid>> UpdateOrder(int id, OrderVm order);
    Task<Response<Guid>> DeleteOrder(int id);
}