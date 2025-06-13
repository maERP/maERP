using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseDetail;

public class WarehouseDetailHandler : IRequestHandler<WarehouseDetailQuery, Result<WarehouseDetailDto>>
{
    private readonly IAppLogger<WarehouseDetailHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IGenericRepository<ProductStock> _productStockRepository;

    public WarehouseDetailHandler(
        IAppLogger<WarehouseDetailHandler> logger,
        IWarehouseRepository warehouseRepository,
        IGenericRepository<ProductStock> productStockRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        _productStockRepository = productStockRepository ?? throw new ArgumentNullException(nameof(productStockRepository));
    }
    
    public async Task<Result<WarehouseDetailDto>> Handle(WarehouseDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving warehouse details for ID: {Id}", request.Id);
        
        var result = new Result<WarehouseDetailDto>();
        
        try
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, true);

            if (warehouse == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Warehouse with ID {request.Id} not found");
                
                _logger.LogWarning("Warehouse with ID {Id} not found", request.Id);
                return result;
            }

            // Anzahl der Produkte in diesem Lager ermitteln
            var productCount = _productStockRepository.Entities
                .Where(ps => ps.WarehouseId == warehouse.Id)
                .Select(ps => ps.ProductId)
                .Distinct()
                .Count();

            // Manuelles Mapping zur DTO-Entität
            var data = new WarehouseDetailDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                ProductCount = productCount
            };

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("Warehouse with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the warehouse: {ex.Message}");
            
            _logger.LogError("Error retrieving warehouse: {Message}", ex.Message);
        }
        
        return result;
    }
}