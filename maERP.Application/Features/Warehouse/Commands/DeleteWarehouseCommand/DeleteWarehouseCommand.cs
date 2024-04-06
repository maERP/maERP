using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.DeleteWarehouseCommand;

public class DeleteWarehouseCommand : IRequest<int>
{
    public int Id { get; set; }     
}