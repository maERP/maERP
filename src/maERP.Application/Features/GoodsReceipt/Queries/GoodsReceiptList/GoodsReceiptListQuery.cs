using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.GoodsReceipt.Queries.GoodsReceiptList;

public class GoodsReceiptListQuery : IRequest<PaginatedResult<GoodsReceiptListDto>>
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 50;
    public string SearchTerm { get; set; } = string.Empty;
    public string OrderBy { get; set; } = "ReceiptDate Descending";
}