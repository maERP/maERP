using maERP.Application.Dtos.Warehouse;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehousesQuery;

public record GetWarehousesQuery : IRequest<List<WarehouseListDto>>;