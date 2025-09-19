using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerDelete;

public class ManufacturerDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}