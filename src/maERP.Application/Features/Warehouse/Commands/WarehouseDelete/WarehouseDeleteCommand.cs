using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }     
}