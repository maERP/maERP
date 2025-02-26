using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

public class WarehouseCreateCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = string.Empty;     
}