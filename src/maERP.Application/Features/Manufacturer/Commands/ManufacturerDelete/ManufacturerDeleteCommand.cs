using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerDelete;

public class ManufacturerDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}