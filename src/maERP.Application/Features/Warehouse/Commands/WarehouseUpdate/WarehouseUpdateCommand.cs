using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;

public class WarehouseUpdateCommand : WarehouseInputDto, IRequest<Result<int>>
{
}