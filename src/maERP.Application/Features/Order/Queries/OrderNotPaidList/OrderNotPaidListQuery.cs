using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Queries.OrderNotPaidList;

public class OrderNotPaidListQuery : IRequest<PaginatedResult<OrderListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string[] OrderBy { get; set; }

    public OrderNotPaidListQuery(int pageNumber = 1, int pageSize = 10, string orderBy = "")
    {
        PageNumber = pageNumber;
        PageSize = pageSize;

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            OrderBy = orderBy.Split(',');
        }
        else OrderBy = Array.Empty<string>();
    }
}