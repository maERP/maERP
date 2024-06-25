using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IOrderService
{
    Task<PaginatedResult<OrderListVM>> GetOrders(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<OrderVM> GetOrderDetails(int id);
    Task<Response<Guid>> CreateOrder(OrderVM order);
    Task<Response<Guid>> UpdateOrder(int id, OrderVM order);
    Task<Response<Guid>> DeleteOrder(int id);
}