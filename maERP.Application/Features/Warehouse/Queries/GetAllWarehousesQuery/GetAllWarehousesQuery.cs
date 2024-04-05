using maERP.Application.Dtos;
using maERP.Application.Dtos.Warehouse;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetAllWarehousesQuery;

public record GetAllWarehousesQuery : IRequest<List<WarehouseListDto>>;