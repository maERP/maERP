using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Queries.TenantList;

public class TenantListQuery : IRequest<PaginatedResult<TenantListDto>>
{
    public string UserId { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] SalesBy { get; set; }

    public TenantListQuery(string userId, int pageNumber = 1, int pageSize = 10, string searchString = "", string salesBy = "")
    {
        UserId = userId;
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchString = searchString;

        if (!string.IsNullOrWhiteSpace(salesBy))
        {
            SalesBy = salesBy.Split(',');
        }
        else
        {
            SalesBy = Array.Empty<string>();
        }
    }
}
