using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassDetail;

/// <summary>
/// Query for retrieving detailed information about a specific tax class.
/// Implements IRequest to work with MediatR, returning tax class details wrapped in a Result.
/// </summary>
public class TaxClassDetailQuery : IRequest<Result<TaxClassDetailDto>>
{
    /// <summary>
    /// The unique identifier of the tax class to retrieve
    /// </summary>
    public Guid Id { get; set; }
}