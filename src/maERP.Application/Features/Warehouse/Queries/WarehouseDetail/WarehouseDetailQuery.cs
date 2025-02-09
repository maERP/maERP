using maERP.Domain.Dtos.Warehouse;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseDetail;

public class WarehouseDetailQuery : IRequest<WarehouseDetailDto>
{
    public int Id { get; set; }
}