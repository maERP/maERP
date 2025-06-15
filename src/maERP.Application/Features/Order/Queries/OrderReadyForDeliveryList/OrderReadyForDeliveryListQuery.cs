using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderReadyForDeliveryList;

public class OrderReadyForDeliveryListQuery : IRequest<PaginatedResult<OrderListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string[] OrderBy { get; set; }

    public OrderReadyForDeliveryListQuery(int pageNumber = 1, int pageSize = 10, string orderBy = "")
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