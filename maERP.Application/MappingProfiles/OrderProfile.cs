using AutoMapper;
using maERP.Domain;
using maERP.Application.Dtos.Order;
using maERP.Application.Features.Order.Commands.CreateOrderCommand;
using maERP.Application.Features.Order.Commands.DeleteOrderCommand;
using maERP.Application.Features.Order.Commands.UpdateOrderCommand;

namespace maERP.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderCreateDto>();
        CreateMap<Order, OrderDetailDto>();
        CreateMap<Order, OrderListDto>();
        CreateMap<Order, OrderUpdateDto>();

        CreateMap<CreateOrderCommand, Order>();
        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<UpdateOrderCommand, Order>();
    }
}