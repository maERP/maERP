using AutoMapper;
using maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;
using maERP.Application.Features.Warehouse.Commands.DeleteWarehouseCommand;
using maERP.Application.Features.Warehouse.Commands.UpdateWarehouseCommand;
using maERP.Application.Features.Warehouse.Queries.GetWarehouseDetails;
using maERP.Application.Features.Warehouse.Queries.GetWarehouses;
using maERP.Domain.Models;

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