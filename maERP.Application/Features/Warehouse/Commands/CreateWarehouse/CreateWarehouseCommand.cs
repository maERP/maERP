using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.CreateWarehouse;

public class CreateWarehouseCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;     
}