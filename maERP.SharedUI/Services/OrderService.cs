using AutoMapper;
using Blazored.LocalStorage;
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

    public async Task<List<OrderVM>> GetOrders()
    {
        await AddBearerToken();
        var orders = await _client.OrdersAllAsync();
        return _mapper.Map<List<OrderVM>>(orders);
    }

    public async Task<OrderVM> GetOrderDetails(int id)
    {
        await AddBearerToken();
        var order = await _client.OrdersGETAsync(id);
        return _mapper.Map<OrderVM>(order);
    }

    public async Task<Response<Guid>> CreateOrder(OrderVM order)
    {
        try
        {
            await AddBearerToken();
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(order);
            await _client.OrdersPOSTAsync(createOrderCommand);
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
            var updateOrderCommand = _mapper.Map<UpdateOrderCommand>(order);
            await _client.OrdersPUTAsync(id.ToString(), updateOrderCommand);
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