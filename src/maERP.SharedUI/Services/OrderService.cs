using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class OrderService : BaseHttpService, IOrderService
{
    private readonly IMapper _mapper;

    public OrderService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<OrderListVm>> GetOrders(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var orders = await Client.OrdersGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<OrderListVm>>(orders);
    }

    public async Task<OrderVm> GetOrderDetails(int id)
    {
        await AddBearerToken();
        var order = await Client.OrdersGET2Async(id);
        return _mapper.Map<OrderVm>(order);
    }

    public async Task<Response<Guid>> CreateOrder(OrderVm order)
    {
        try
        {
            await AddBearerToken();
            var orderCreateCommand = _mapper.Map<OrderCreateCommand>(order);
            await Client.OrdersPOSTAsync(orderCreateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateOrder(int id, OrderVm order)
    {
        try
        {
            await AddBearerToken();
            var orderUpdateCommand = _mapper.Map<OrderUpdateCommand>(order);
            await Client.OrdersPUTAsync(id, orderUpdateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
    public async Task<Response<Guid>> DeleteOrder(int id)
    {
        try
        {
            await AddBearerToken();
            await Client.OrdersDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}