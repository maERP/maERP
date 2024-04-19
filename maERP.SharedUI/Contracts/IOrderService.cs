using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IOrderService
{
    Task<List<OrderVM>> GetOrders();
    Task<OrderVM> GetOrderDetails(int id);
    Task<Response<Guid>> CreateOrder(OrderVM order);
    Task<Response<Guid>> UpdateOrder(int id, OrderVM order);
    Task<Response<Guid>> DeleteOrder(int id);
}