using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public int? NewWarehouseId { get; set; }
}