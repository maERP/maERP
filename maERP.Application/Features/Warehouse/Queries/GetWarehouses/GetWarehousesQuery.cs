using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouses;

public record GetWarehousesQuery : IRequest<List<WarehouseListDto>>;