using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Warehouse.Queries.WarehouseList;

// ReSharper disable once UnusedType.Global
public class WarehouseListHandler : IRequestHandler<WarehouseListQuery, PaginatedResult<WarehouseListDto>>
{
    private readonly IAppLogger<WarehouseListHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseListHandler(
        IAppLogger<WarehouseListHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }
    public async Task<PaginatedResult<WarehouseListDto>> Handle(WarehouseListQuery request, CancellationToken cancellationToken)
    {
        var warehouseFilterSpec = new WarehouseFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle WarehouseListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _warehouseRepository.Entities
               .AsNoTracking()
               .Specify(warehouseFilterSpec)
               .Select(w => new WarehouseListDto
               {
                   Id = w.Id,
                   Name = w.Name
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _warehouseRepository.Entities
            .AsNoTracking()
            .Specify(warehouseFilterSpec)
            .OrderBy(ordering)
            .Select(w => new WarehouseListDto
            {
                Id = w.Id,
                Name = w.Name
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}