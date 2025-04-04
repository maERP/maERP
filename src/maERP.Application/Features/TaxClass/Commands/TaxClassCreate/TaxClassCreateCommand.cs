using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

/// <summary>
/// Command for creating a new tax class in the system.
/// Inherits from TaxClassInputDto to get all tax class properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created tax class wrapped in a Result.
/// </summary>
public class TaxClassCreateCommand : TaxClassInputDto, IRequest<Result<int>>
{
}