using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerCreate;

/// <summary>
/// Command for creating a new manufacturer in the system.
/// Inherits from ManufacturerInputDto to get all manufacturer properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created manufacturer wrapped in a Result.
/// </summary>
public class ManufacturerCreateCommand : ManufacturerInputDto, IRequest<Result<int>>
{
}