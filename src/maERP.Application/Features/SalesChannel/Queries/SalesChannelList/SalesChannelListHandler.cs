using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

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

        if (request.OrderBy.Any() != true)
        {
            return await _salesChannelRepository.Entities
               .Specify(salesChannelFilterSpec)
               .Select(entity => new SalesChannelListDto
               {
                   Id = entity.Id,
                   SalesChannelType = entity.Type,
                   Name = entity.Name,
                   DateCreated = entity.DateCreated,
                   Url = entity.Url,
                   Username = entity.Username,
                   ImportProducts = entity.ImportProducts,
                   ImportCustomers = entity.ImportCustomers,
                   ImportOrders = entity.ImportOrders,
                   ExportProducts = entity.ExportProducts,
                   ExportCustomers = entity.ExportCustomers,
                   ExportOrders = entity.ExportOrders,
                   Warehouses = entity.Warehouses.Select(w => new WarehouseDetailDto
                   {
                       Id = w.Id,
                       Name = w.Name
                   }).ToList()
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _salesChannelRepository.Entities
            .Specify(salesChannelFilterSpec)
            .OrderBy(ordering)
            .Select(entity => new SalesChannelListDto
            {
                Id = entity.Id,
                SalesChannelType = entity.Type,
                Name = entity.Name,
                DateCreated = entity.DateCreated,
                Url = entity.Url,
                Username = entity.Username,
                ImportProducts = entity.ImportProducts,
                ImportCustomers = entity.ImportCustomers,
                ImportOrders = entity.ImportOrders,
                ExportProducts = entity.ExportProducts,
                ExportCustomers = entity.ExportCustomers,
                ExportOrders = entity.ExportOrders,
                Warehouses = entity.Warehouses.Select(w => new WarehouseDetailDto
                {
                    Id = w.Id,
                    Name = w.Name
                }).ToList()
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}