using AutoMapper;
using maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;
using maERP.Application.Features.Warehouse.Commands.DeleteWarehouseCommand;
using maERP.Application.Features.Warehouse.Commands.UpdateWarehouseCommand;
using maERP.Domain.Models;
using maERP.Application.Dtos.Warehouse;

namespace maERP.Application.MappingProfiles;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<Warehouse, WarehouseCreateDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseDetailDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseListDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseUpdateDto>().ReverseMap();

        CreateMap<CreateWarehouseCommand, Warehouse>();
        CreateMap<DeleteWarehouseCommand, Warehouse>();
        CreateMap<UpdateWarehouseCommand, Warehouse>();
    }
}