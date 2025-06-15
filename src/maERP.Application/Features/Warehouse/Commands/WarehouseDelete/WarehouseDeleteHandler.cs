using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteHandler : IRequestHandler<WarehouseDeleteCommand, Result<int>>
{
    private readonly IAppLogger<WarehouseDeleteHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly IGenericRepository<ProductStock> _productStockRepository;

    public WarehouseDeleteHandler(
        IAppLogger<WarehouseDeleteHandler> logger,
        IWarehouseRepository warehouseRepository,
        ISalesChannelRepository salesChannelRepository,
        IGenericRepository<ProductStock> productStockRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
        _productStockRepository = productStockRepository ?? throw new ArgumentNullException(nameof(productStockRepository));
    }

    public async Task<Result<int>> Handle(WarehouseDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting warehouse with ID: {Id}", request.Id);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new WarehouseDeleteValidator(_warehouseRepository, _salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(WarehouseDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Handle product redistribution if NewWarehouseId is provided
            if (request.NewWarehouseId.HasValue)
            {
                _logger.LogInformation("Redistributing products from warehouse {OldId} to warehouse {NewId}",
                    request.Id, request.NewWarehouseId.Value);

                // Validate that the target warehouse exists
                var targetWarehouse = await _warehouseRepository.GetByIdAsync(request.NewWarehouseId.Value);
                if (targetWarehouse == null)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add($"Target warehouse with ID {request.NewWarehouseId.Value} not found");
                    return result;
                }

                // Get all product stocks for the warehouse to be deleted
                var productStocks = _productStockRepository.Entities
                    .Where(ps => ps.WarehouseId == request.Id)
                    .ToList();

                foreach (var productStock in productStocks)
                {
                    // Check if there's already a stock entry for this product in the target warehouse
                    var existingStock = _productStockRepository.Entities
                        .FirstOrDefault(ps => ps.ProductId == productStock.ProductId &&
                                            ps.WarehouseId == request.NewWarehouseId.Value);

                    if (existingStock != null)
                    {
                        // Add to existing stock
                        existingStock.Stock += productStock.Stock;
                        await _productStockRepository.UpdateAsync(existingStock);
                    }
                    else
                    {
                        // Create new stock entry in target warehouse
                        var newStock = new ProductStock
                        {
                            ProductId = productStock.ProductId,
                            WarehouseId = request.NewWarehouseId.Value,
                            Stock = productStock.Stock
                        };
                        await _productStockRepository.CreateAsync(newStock);
                    }

                    // Delete the old stock entry
                    await _productStockRepository.DeleteAsync(productStock);
                }

                _logger.LogInformation("Successfully redistributed {Count} product stocks", productStocks.Count);
            }
            else
            {
                // If no target warehouse specified, delete all product stocks for this warehouse
                var productStocks = _productStockRepository.Entities
                    .Where(ps => ps.WarehouseId == request.Id)
                    .ToList();

                foreach (var productStock in productStocks)
                {
                    await _productStockRepository.DeleteAsync(productStock);
                }

                _logger.LogInformation("Deleted {Count} product stock entries", productStocks.Count);
            }

            // Create entity to delete
            var warehouseToDelete = new Domain.Entities.Warehouse
            {
                Id = request.Id
            };

            // Delete from database
            await _warehouseRepository.DeleteAsync(warehouseToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = warehouseToDelete.Id;

            _logger.LogInformation("Successfully deleted warehouse with ID: {Id}", warehouseToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the warehouse: {ex.Message}");

            _logger.LogError("Error deleting warehouse: {Message}", ex.Message);
        }

        return result;
    }
}
