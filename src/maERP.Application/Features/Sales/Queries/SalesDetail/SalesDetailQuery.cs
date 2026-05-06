using maERP.Domain.Dtos.Sales;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Queries.SalesDetail;

/// <summary>
/// Query for retrieving detailed information about a specific sales.
/// Implements IRequest to work with MediatR, returning sales details wrapped in a Result.
/// </summary>
public class SalesDetailQuery : IRequest<Result<SalesDetailDto>>
{
    /// <summary>
    /// The unique identifier of the sales to retrieve
    /// </summary>
    public Guid Id { get; set; }
}