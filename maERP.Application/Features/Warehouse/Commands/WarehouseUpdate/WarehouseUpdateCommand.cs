using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;

public class WarehouseUpdateCommand : IRequest<int>
{
    public int Id { get; set; }     
    public string Name { get; set; } = string.Empty;
}