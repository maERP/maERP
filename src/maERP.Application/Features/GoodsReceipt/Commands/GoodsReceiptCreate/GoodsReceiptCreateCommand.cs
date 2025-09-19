using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.GoodsReceipt.Commands.GoodsReceiptCreate;

public class GoodsReceiptCreateCommand : GoodsReceiptInputDto, IRequest<Result<Guid>>
{
}