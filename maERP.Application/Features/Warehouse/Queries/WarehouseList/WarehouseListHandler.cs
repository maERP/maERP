using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseList;

public class WarehouseListHandler : IRequestHandler<WarehouseListQuery, List<WarehouseListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<WarehouseListHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseListHandler(IMapper mapper,
        IAppLogger<WarehouseListHandler> logger, 
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository; 
    }
    public async Task<List<WarehouseListResponse>> Handle(WarehouseListQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var warehouses = await _warehouseRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<WarehouseListResponse>>(warehouses);

        // Return list of DTO objects
        _logger.LogInformation("All Warehouses are retrieved successfully.");
        return data;
    }
}