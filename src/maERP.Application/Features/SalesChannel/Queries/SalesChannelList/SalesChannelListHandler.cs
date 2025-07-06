using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelList;

public class SalesChannelListHandler : IRequestHandler<SalesChannelListQuery, PaginatedResult<SalesChannelListDto>>
{
    private readonly IAppLogger<SalesChannelListHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelListHandler(
        IAppLogger<SalesChannelListHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<PaginatedResult<SalesChannelListDto>> Handle(SalesChannelListQuery request, CancellationToken cancellationToken)
    {
        var salesChannelFilterSpec = new SalesChannelFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle SalesChannelListQuery: {0}", request);

        List<Domain.Entities.SalesChannel> entities;

        if (request.OrderBy.Any() != true)
        {
            entities = await _salesChannelRepository.Entities
               .Specify(salesChannelFilterSpec)
               .ToListAsync(cancellationToken);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            entities = await _salesChannelRepository.Entities
                .Specify(salesChannelFilterSpec)
                .OrderBy(ordering)
                .ToListAsync(cancellationToken);
        }

        return MapToListDtoAndPaginate(entities, request.PageNumber, request.PageSize);
    }

    private PaginatedResult<SalesChannelListDto> MapToListDtoAndPaginate(
        List<Domain.Entities.SalesChannel> entities, int pageNumber, int pageSize)
    {
        var dtos = entities.Select(entity => new SalesChannelListDto
        {
            Id = entity.Id,
            SalesChannelType = entity.Type,
            Name = entity.Name,
            Url = entity.Url,
            Username = entity.Username,
            ImportProducts = entity.ImportProducts,
            ImportCustomers = entity.ImportCustomers,
            ImportOrders = entity.ImportOrders,
            ExportProducts = entity.ExportProducts,
            ExportCustomers = entity.ExportCustomers,
            ExportOrders = entity.ExportOrders,
            Warehouses = entity.Warehouses?.Select(w => new WarehouseDetailDto
            {
                Id = w.Id,
                Name = w.Name
            }).ToList() ?? new List<WarehouseDetailDto>()
        }).ToList();

        // Erstelle paginierte Ergebnisse
        var totalCount = dtos.Count;
        var pagedItems = dtos
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return PaginatedResult<SalesChannelListDto>.Success(pagedItems, totalCount, pageNumber, pageSize);
    }
}