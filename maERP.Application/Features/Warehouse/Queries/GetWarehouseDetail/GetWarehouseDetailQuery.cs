using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseDetail;

public class GetWarehouseDetailQuery : IRequest<GetWarehouseDetailResponse>
{
    public int Id { get; set; }
}