using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseDetail;

public class WarehouseDetailQuery : IRequest<WarehouseDetailResponse>
{
    public int Id { get; set; }
}