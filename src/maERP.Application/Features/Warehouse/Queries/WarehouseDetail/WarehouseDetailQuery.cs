using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseDetail;

public class WarehouseDetailQuery : IRequest<Result<WarehouseDetailDto>>
{
    public int Id { get; set; }
}