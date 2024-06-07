using AutoMapper;
using maERP.Application.Features.Order.Commands.CreateOrder;
using maERP.Application.Features.Order.Commands.DeleteOrder;
using maERP.Application.Features.Order.Commands.UpdateOrder;
using maERP.Application.Features.Order.Queries.GetOrderDetail;
using maERP.Application.Features.Order.Queries.GetOrders;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, CreateOrderResponse>();
        CreateMap<Order, GetOrderDetailResponse>();
        CreateMap<Order, GetOrdersResponse>();
        CreateMap<Order, UpdateOrderResponse>();

        CreateMap<CreateOrderCommand, Order>();
        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<UpdateOrderCommand, Order>();
    }
}