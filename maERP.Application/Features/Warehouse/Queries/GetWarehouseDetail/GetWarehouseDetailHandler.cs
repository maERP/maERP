using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.GetWarehouseDetail;

public class GetWarehouseDetailHandler : IRequestHandler<GetWarehouseDetailQuery, GetWarehouseDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetWarehouseDetailHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseDetailHandler(IMapper mapper,
        IAppLogger<GetWarehouseDetailHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }
    public async Task<GetWarehouseDetailResponse> Handle(GetWarehouseDetailQuery request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, true);

        if (warehouse == null)
        {
            throw new NotFoundException("NotFoundException", "warehouse not found.");
        }

        var data = _mapper.Map<GetWarehouseDetailResponse>(warehouse);

        _logger.LogInformation("All Warehouses are retrieved successfully.");
        return data;
    }
}