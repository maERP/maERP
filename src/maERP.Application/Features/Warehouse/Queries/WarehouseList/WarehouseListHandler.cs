using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseList;

// ReSharper disable once UnusedType.Global
public class WarehouseListHandler : IRequestHandler<WarehouseListQuery, PaginatedResult<WarehouseListResponse>>
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
    public async Task<PaginatedResult<WarehouseListResponse>> Handle(WarehouseListQuery request, CancellationToken cancellationToken)
    {
        var warehouseFilterSpec = new WarehouseFilterSpecification(request.SearchString);
        
        _logger.LogInformation("Handle WarehouseListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _warehouseRepository.Entities
               .Specify(warehouseFilterSpec)
               .ProjectTo<WarehouseListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _warehouseRepository.Entities
               .Specify(warehouseFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<WarehouseListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}