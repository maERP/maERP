using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.DeleteWarehouseCommand;

public class DeleteTaxClassCommand : IRequest<int>
{
    public int Id { get; set; }     
}