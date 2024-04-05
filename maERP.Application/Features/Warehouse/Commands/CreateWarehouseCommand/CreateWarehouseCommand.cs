using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;

public class CreateTaxClassCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;     
}