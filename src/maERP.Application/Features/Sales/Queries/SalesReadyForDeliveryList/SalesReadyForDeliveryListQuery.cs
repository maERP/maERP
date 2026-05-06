using maERP.Domain.Dtos.Sales;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Queries.SalesReadyForDeliveryList;

public class SalesReadyForDeliveryListQuery : IRequest<PaginatedResult<SalesListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string[] SalesBy { get; set; }

    public SalesReadyForDeliveryListQuery(int pageNumber = 1, int pageSize = 10, string salesBy = "")
    {
        PageNumber = pageNumber;
        PageSize = pageSize;

        if (!string.IsNullOrWhiteSpace(salesBy))
        {
            SalesBy = salesBy.Split(',');
        }
        else SalesBy = Array.Empty<string>();
    }
}