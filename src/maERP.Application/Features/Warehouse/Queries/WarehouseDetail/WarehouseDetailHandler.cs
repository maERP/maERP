﻿using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Dtos.Warehouse;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseDetail;

public class WarehouseDetailHandler : IRequestHandler<WarehouseDetailQuery, WarehouseDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<WarehouseDetailHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseDetailHandler(IMapper mapper,
        IAppLogger<WarehouseDetailHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }
    public async Task<WarehouseDetailDto> Handle(WarehouseDetailQuery request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, true);

        if (warehouse == null)
        {
            throw new NotFoundException("NotFoundException", "warehouse not found.");
        }

        var data = _mapper.Map<WarehouseDetailDto>(warehouse);

        _logger.LogInformation("Warehous retrieved successfully.");
        return data;
    }
}