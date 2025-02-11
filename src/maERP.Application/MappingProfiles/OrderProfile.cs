using AutoMapper;
using maERP.Application.Features.Order.Commands.OrderCreate;
using maERP.Application.Features.Order.Commands.OrderDelete;
using maERP.Application.Features.Order.Commands.OrderUpdate;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDetailDto>();
        CreateMap<Order, OrderListDto>();

        CreateMap<OrderCreateCommand, Order>();
        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<OrderUpdateCommand, Order>();
    }
}