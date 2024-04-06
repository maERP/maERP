using AutoMapper;
using maERP.Domain;
using maERP.Application.Dtos.Warehouse;
using maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;

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
    }
}
