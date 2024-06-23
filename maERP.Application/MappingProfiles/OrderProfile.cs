using AutoMapper;
using maERP.Application.Features.Order.Commands.OrderCreate;
using maERP.Application.Features.Order.Commands.OrderDelete;
using maERP.Application.Features.Order.Commands.OrderUpdate;
using maERP.Application.Features.Order.Queries.OrderDetail;
using maERP.Application.Features.Order.Queries.OrderList;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderCreateResponse>();
        CreateMap<Order, OrderDetailResponse>();
        CreateMap<Order, OrderListResponse>();
        CreateMap<Order, OrderUpdateResponse>();

        CreateMap<OrderCreateCommand, Order>();
        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<OrderUpdateCommand, Order>();
    }
}