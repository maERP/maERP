using maERP.Application.Dtos.Warehouse;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseQuery;

public class GetWarehouseQuery : IRequest<WarehouseDetailDto>
{
    public int Id { get; set; }
}