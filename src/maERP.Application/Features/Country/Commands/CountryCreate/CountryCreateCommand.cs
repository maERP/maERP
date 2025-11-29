using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Commands.CountryCreate;

/// <summary>
/// Command for creating a new country in the system.
/// Inherits from CountryInputDto to get all country properties and implements IRequest
/// to work with the custom mediator, returning the ID of the newly created country wrapped in a Result.
/// </summary>
public class CountryCreateCommand : CountryInputDto, IRequest<Result<Guid>>
{
}
