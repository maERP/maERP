using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.GoodsReceipt.Queries.GoodsReceiptList;

public class GoodsReceiptListHandler : IRequestHandler<GoodsReceiptListQuery, PaginatedResult<GoodsReceiptListDto>>
{
    private readonly IAppLogger<GoodsReceiptListHandler> _logger;
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;

    public GoodsReceiptListHandler(
        IAppLogger<GoodsReceiptListHandler> logger,
        IGoodsReceiptRepository goodsReceiptRepository)
    {
        _logger = logger;
        _goodsReceiptRepository = goodsReceiptRepository;
    }

    public async Task<PaginatedResult<GoodsReceiptListDto>> Handle(GoodsReceiptListQuery request, CancellationToken cancellationToken)
    {
        var filterSpec = new GoodsReceiptFilterSpecification(request.SearchTerm);

        _logger.LogInformation("Handle GoodsReceiptListQuery: {0}", request);

        if (string.IsNullOrEmpty(request.OrderBy))
        {
            var goodsReceipts = await _goodsReceiptRepository.Entities
               .Specify(filterSpec)
               .Select(gr => MapToGoodsReceiptListDto(gr))
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return goodsReceipts;
        }

        var orderedGoodsReceipts = await _goodsReceiptRepository.Entities
            .Specify(filterSpec)
            .OrderBy(request.OrderBy)
            .Select(gr => MapToGoodsReceiptListDto(gr))
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return orderedGoodsReceipts;
    }

    private static GoodsReceiptListDto MapToGoodsReceiptListDto(Domain.Entities.GoodsReceipt goodsReceipt)
    {
        return new GoodsReceiptListDto
        {
            Id = goodsReceipt.Id,
            ReceiptDate = goodsReceipt.ReceiptDate,
            ProductName = goodsReceipt.Product?.Name ?? "Unknown Product",
            ProductSku = goodsReceipt.Product?.Sku ?? "Unknown SKU",
            Quantity = goodsReceipt.Quantity,
            WarehouseName = goodsReceipt.Warehouse?.Name ?? "Unknown Warehouse",
            Supplier = goodsReceipt.Supplier,
            CreatedBy = goodsReceipt.CreatedBy,
            DateCreated = goodsReceipt.DateCreated
        };
    }
}