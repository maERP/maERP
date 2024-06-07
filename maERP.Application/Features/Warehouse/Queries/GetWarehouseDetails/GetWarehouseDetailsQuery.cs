using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseDetails;

public class GetWarehouseDetailQuery : IRequest<WarehouseDetailDto>
{
    public int Id { get; set; }
}