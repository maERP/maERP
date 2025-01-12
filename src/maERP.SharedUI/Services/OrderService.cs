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

    public async Task<PaginatedResult<OrderListVM>> GetOrders(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var orders = await _client.OrdersGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<OrderListVM>>(orders);
    }

    public async Task<OrderVM> GetOrderDetails(int id)
    {
        await AddBearerToken();
        var order = await _client.OrdersGET2Async(id);
        return _mapper.Map<OrderVM>(order);
    }

    public async Task<Response<Guid>> CreateOrder(OrderVM order)
    {
        try
        {
            await AddBearerToken();
            var orderCreateCommand = _mapper.Map<OrderCreateCommand>(order);
            await _client.OrdersPOSTAsync(orderCreateCommand);
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

    public async Task<Response<Guid>> UpdateOrder(int id, OrderVM order)
    {
        try
        {
            await AddBearerToken();
            var orderUpdateCommand = _mapper.Map<OrderUpdateCommand>(order);
            await _client.OrdersPUTAsync(id, orderUpdateCommand);
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
            await _client.OrdersDELETEAsync(id);
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