using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}