using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Warehouse;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseDetailQuery;

public class GetWarehouseDetailQueryHandler : IRequestHandler<GetWarehouseDetailQuery, WarehouseDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetWarehouseDetailQueryHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseDetailQueryHandler(IMapper mapper,
        IAppLogger<GetWarehouseDetailQueryHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }
    public async Task<WarehouseDetailDto> Handle(GetWarehouseDetailQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id);

        if (warehouse == null)
        {
            throw new NotFoundException("NotFoundException", "warehouse not found.");
        }

        // Convert data objects to DTO objects
        var data = _mapper.Map<WarehouseDetailDto>(warehouse);

        // Return list of DTO objects
        _logger.LogInformation("All Warehouses are retrieved successfully.");
        return data;
    }
}