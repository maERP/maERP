using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseList;

public record WarehouseListQuery : IRequest<List<WarehouseListResponse>>;