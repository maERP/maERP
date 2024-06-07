using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.DeleteWarehouse;

public class DeleteWarehouseCommand : IRequest<int>
{
    public int Id { get; set; }     
}