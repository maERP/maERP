using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Warehouse;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseQuery;

public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, WarehouseDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetWarehouseQueryHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseQueryHandler(IMapper mapper,
        IAppLogger<GetWarehouseQueryHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }
    public async Task<WarehouseDetailDto> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<WarehouseDetailDto>(warehouse);

        // Return list of DTO objects
        _logger.LogInformation("All Warehouses are retrieved successfully.");
        return data;
    }
}