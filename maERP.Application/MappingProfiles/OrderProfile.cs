using AutoMapper;
using maERP.Application.Dtos.Order;
using maERP.Application.Features.Order.Commands.CreateOrder;
using maERP.Application.Features.Order.Commands.DeleteOrder;
using maERP.Application.Features.Order.Commands.UpdateOrder;
using maERP.Application.Features.Order.Queries.GetOrdersQuery;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderCreateDto>();
        CreateMap<Order, OrderDetailDto>();
        CreateMap<Order, GetOrdersResponse>();
        CreateMap<Order, OrderUpdateDto>();

        CreateMap<CreateOrderCommand, Order>();
        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<UpdateOrderCommand, Order>();
    }
}