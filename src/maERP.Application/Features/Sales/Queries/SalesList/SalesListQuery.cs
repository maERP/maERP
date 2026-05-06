using maERP.Domain.Dtos.Sales;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Queries.SalesList;

public class SalesListQuery : IRequest<PaginatedResult<SalesListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] SalesBy { get; set; }
    public Guid? SalesChannelId { get; set; }

    public SalesListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string salesBy = "", Guid? salesChannelId = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchString = searchString;
        SalesChannelId = salesChannelId;

        if (!string.IsNullOrWhiteSpace(salesBy))
        {
            SalesBy = salesBy.Split(',');
        }
        else SalesBy = new string[] { };
    }
}