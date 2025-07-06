using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.GoodsReceipt.Queries.GoodsReceiptDetail;

public class GoodsReceiptDetailHandler : IRequestHandler<GoodsReceiptDetailQuery, Result<GoodsReceiptDetailDto>>
{
    private readonly IAppLogger<GoodsReceiptDetailHandler> _logger;
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;

    public GoodsReceiptDetailHandler(
        IAppLogger<GoodsReceiptDetailHandler> logger,
        IGoodsReceiptRepository goodsReceiptRepository)
    {
        _logger = logger;
        _goodsReceiptRepository = goodsReceiptRepository;
    }

    public async Task<Result<GoodsReceiptDetailDto>> Handle(GoodsReceiptDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle GoodsReceiptDetailQuery for ID: {Id}", request.Id);

        var result = new Result<GoodsReceiptDetailDto>();

        try
        {
            var goodsReceipt = await _goodsReceiptRepository.GetByIdWithDetailsAsync(request.Id);

            if (goodsReceipt == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Goods receipt with ID {request.Id} not found.");
                return result;
            }

            var dto = MapToGoodsReceiptDetailDto(goodsReceipt);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = dto;

            _logger.LogInformation("Successfully retrieved goods receipt details for ID: {Id}", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving goods receipt details: {ex.Message}");

            _logger.LogError("Error retrieving goods receipt details for ID {Id}: {Message}", request.Id, ex.Message);
        }

        return result;
    }

    private static GoodsReceiptDetailDto MapToGoodsReceiptDetailDto(Domain.Entities.GoodsReceipt goodsReceipt)
    {
        return new GoodsReceiptDetailDto
        {
            Id = goodsReceipt.Id,
            ReceiptDate = goodsReceipt.ReceiptDate,
            ProductId = goodsReceipt.ProductId,
            ProductName = goodsReceipt.Product?.Name ?? "Unknown Product",
            ProductSku = goodsReceipt.Product?.Sku ?? "Unknown SKU",
            Quantity = goodsReceipt.Quantity,
            WarehouseId = goodsReceipt.WarehouseId,
            WarehouseName = goodsReceipt.Warehouse?.Name ?? "Unknown Warehouse",
            Supplier = goodsReceipt.Supplier,
            Notes = goodsReceipt.Notes,
            CreatedBy = goodsReceipt.CreatedBy,
            DateCreated = goodsReceipt.DateCreated,
            DateModified = goodsReceipt.DateModified
        };
    }
}