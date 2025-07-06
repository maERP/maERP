using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.GoodsReceipt.Commands.GoodsReceiptCreate;

public class GoodsReceiptCreateHandler : IRequestHandler<GoodsReceiptCreateCommand, Result<int>>
{
    private readonly IAppLogger<GoodsReceiptCreateHandler> _logger;
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public GoodsReceiptCreateHandler(
        IAppLogger<GoodsReceiptCreateHandler> logger,
        IGoodsReceiptRepository goodsReceiptRepository,
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _goodsReceiptRepository = goodsReceiptRepository ?? throw new ArgumentNullException(nameof(goodsReceiptRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
    }

    public async Task<Result<int>> Handle(GoodsReceiptCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new goods receipt for Product ID: {ProductId}, Quantity: {Quantity}", 
            request.ProductId, request.Quantity);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new GoodsReceiptCreateValidator(_productRepository, _warehouseRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(GoodsReceiptCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manual mapping
            var goodsReceiptToCreate = new Domain.Entities.GoodsReceipt
            {
                ReceiptDate = request.ReceiptDate,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                WarehouseId = request.WarehouseId,
                Supplier = request.Supplier,
                Notes = request.Notes,
                CreatedBy = "System" // TODO: Get from current user context
            };

            // Add the new goods receipt to the database
            await _goodsReceiptRepository.CreateAsync(goodsReceiptToCreate);

            // Update product stock
            await UpdateProductStock(request.ProductId, request.WarehouseId, request.Quantity);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = goodsReceiptToCreate.Id;

            _logger.LogInformation("Successfully created goods receipt with ID: {Id}", goodsReceiptToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the goods receipt: {ex.Message}");

            _logger.LogError("Error creating goods receipt: {Message}", ex.Message);
        }

        return result;
    }

    private async Task UpdateProductStock(int productId, int warehouseId, int quantity)
    {
        try
        {
            // Get existing product stock or create new one
            var existingStock = await _goodsReceiptRepository.GetProductStockAsync(productId, warehouseId);
            
            if (existingStock != null)
            {
                existingStock.Stock += quantity;
                await _goodsReceiptRepository.UpdateProductStockAsync(existingStock);
            }
            else
            {
                var newStock = new Domain.Entities.ProductStock
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Stock = quantity
                };
                await _goodsReceiptRepository.CreateProductStockAsync(newStock);
            }

            _logger.LogInformation("Updated product stock for Product ID: {ProductId}, Warehouse ID: {WarehouseId}, Added Quantity: {Quantity}", 
                productId, warehouseId, quantity);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating product stock: {Message}", ex.Message);
            throw;
        }
    }
}