using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetAllWarehouses;

public record GetAllWarehousesQuery : IRequest<List<WarehouseListDto>>;