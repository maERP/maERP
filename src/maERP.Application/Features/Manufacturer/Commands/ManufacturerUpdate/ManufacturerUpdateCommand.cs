using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerUpdate;

public class ManufacturerUpdateCommand : ManufacturerInputDto, IRequest<Result<int>>
{
}