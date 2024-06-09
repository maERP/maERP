﻿using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouses;

public class GetWarehousesHandler : IRequestHandler<GetWarehousesQuery, List<GetWarehousesResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetWarehousesHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehousesHandler(IMapper mapper,
        IAppLogger<GetWarehousesHandler> logger, 
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository; 
    }
    public async Task<List<GetWarehousesResponse>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var warehouses = await _warehouseRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<GetWarehousesResponse>>(warehouses);

        // Return list of DTO objects
        _logger.LogInformation("All Warehouses are retrieved successfully.");
        return data;
    }
}