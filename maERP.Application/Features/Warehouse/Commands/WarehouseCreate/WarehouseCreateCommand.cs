using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

public class WarehouseCreateCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;     
}