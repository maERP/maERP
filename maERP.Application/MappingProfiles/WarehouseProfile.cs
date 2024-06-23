using AutoMapper;
using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseDelete;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Application.Features.Warehouse.Queries.WarehouseDetail;
using maERP.Application.Features.Warehouse.Queries.WarehouseList;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<Warehouse, WarehouseCreateResponse>().ReverseMap();
        CreateMap<Warehouse, WarehouseDetailResponse>().ReverseMap();
        CreateMap<Warehouse, WarehouseListResponse>().ReverseMap();
        CreateMap<Warehouse, WarehouseUpdateResponse>().ReverseMap();

        CreateMap<WarehouseCreateCommand, Warehouse>();
        CreateMap<WarehouseDeleteCommand, Warehouse>();
        CreateMap<WarehouseUpdateCommand, Warehouse>();
    }
}