using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseDetail;

/// <summary>
/// Query for retrieving detailed information about a specific warehouse.
/// Implements IRequest to work with MediatR, returning warehouse details wrapped in a Result.
/// </summary>
public class WarehouseDetailQuery : IRequest<Result<WarehouseDetailDto>>
{
    /// <summary>
    /// The unique identifier of the warehouse to retrieve
    /// </summary>
    public int Id { get; set; }
}