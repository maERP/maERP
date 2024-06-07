using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseDetails;

public class GetWarehouseDetailsHandler : IRequestHandler<GetWarehouseDetailQuery, WarehouseDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetWarehouseDetailsHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseDetailsHandler(IMapper mapper,
        IAppLogger<GetWarehouseDetailsHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }
    public async Task<WarehouseDetailDto> Handle(GetWarehouseDetailQuery request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, true);

        if (warehouse == null)
        {
            throw new NotFoundException("NotFoundException", "warehouse not found.");
        }

        var data = _mapper.Map<WarehouseDetailDto>(warehouse);

        _logger.LogInformation("All Warehouses are retrieved successfully.");
        return data;
    }
}