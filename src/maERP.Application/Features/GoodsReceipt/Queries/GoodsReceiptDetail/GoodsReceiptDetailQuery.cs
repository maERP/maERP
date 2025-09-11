using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.GoodsReceipt.Queries.GoodsReceiptDetail;

public class GoodsReceiptDetailQuery : IRequest<Result<GoodsReceiptDetailDto>>
{
    public Guid Id { get; set; }
}