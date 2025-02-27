using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

public class WarehouseCreateCommand : WarehouseCreateDto, IRequest<Result<int>>
{
}