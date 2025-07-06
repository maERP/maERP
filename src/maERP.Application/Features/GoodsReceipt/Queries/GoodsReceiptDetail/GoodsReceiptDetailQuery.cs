using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.GoodsReceipt.Queries.GoodsReceiptDetail;

public class GoodsReceiptDetailQuery : IRequest<Result<GoodsReceiptDetailDto>>
{
    public int Id { get; set; }
}