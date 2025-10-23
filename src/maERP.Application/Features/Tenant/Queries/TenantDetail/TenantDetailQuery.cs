using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Queries.TenantDetail;

/// <summary>
/// Query for retrieving detailed information about a specific tenant.
/// Implements IRequest to work with MediatR, returning tenant details wrapped in a Result.
/// </summary>
public class TenantDetailQuery : IRequest<Result<TenantDetailDto>>
{
    /// <summary>
    /// The unique identifier of the tenant to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user ID requesting the tenant details (for authorization)
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
