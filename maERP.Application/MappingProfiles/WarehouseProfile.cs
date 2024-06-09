using AutoMapper;
using maERP.Application.Features.Warehouse.Commands.CreateWarehouse;
using maERP.Application.Features.Warehouse.Commands.DeleteWarehouse;
using maERP.Application.Features.Warehouse.Commands.UpdateWarehouse;
using maERP.Application.Features.Warehouse.Queries.GetWarehouseDetail;
using maERP.Application.Features.Warehouse.Queries.GetWarehouses;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<Warehouse, CreateWarehouseResponse>().ReverseMap();
        CreateMap<Warehouse, GetWarehouseDetailResponse>().ReverseMap();
        CreateMap<Warehouse, GetWarehousesResponse>().ReverseMap();
        CreateMap<Warehouse, UpdateWarehouseResponse>().ReverseMap();

        CreateMap<CreateWarehouseCommand, Warehouse>();
        CreateMap<DeleteWarehouseCommand, Warehouse>();
        CreateMap<UpdateWarehouseCommand, Warehouse>();
    }
}