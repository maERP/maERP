using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public Guid? NewWarehouseId { get; set; }
}