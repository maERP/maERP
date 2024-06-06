using maERP.Application.Dtos.Warehouse;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseDetailQuery;

public class GetWarehouseDetailQuery : IRequest<WarehouseDetailDto>
{
    public int Id { get; set; }
}