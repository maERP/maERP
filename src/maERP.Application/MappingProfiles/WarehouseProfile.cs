using AutoMapper;
using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseDelete;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<Warehouse, WarehouseDetailDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseListDto>().ReverseMap();

        CreateMap<WarehouseCreateCommand, Warehouse>();
        CreateMap<WarehouseDeleteCommand, Warehouse>();
        CreateMap<WarehouseInputCommand, Warehouse>();
    }
}