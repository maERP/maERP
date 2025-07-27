using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Queries.ManufacturerList;

public class ManufacturerListHandler : IRequestHandler<ManufacturerListQuery, PaginatedResult<ManufacturerListDto>>
{
    private readonly IAppLogger<ManufacturerListHandler> _logger;
    private readonly IManufacturerRepository _manufacturerRepository;

    public ManufacturerListHandler(
        IAppLogger<ManufacturerListHandler> logger,
        IManufacturerRepository manufacturerRepository)
    {
        _logger = logger;
        _manufacturerRepository = manufacturerRepository;
    }
    
    public async Task<PaginatedResult<ManufacturerListDto>> Handle(ManufacturerListQuery request, CancellationToken cancellationToken)
    {
        var manufacturerFilterSpec = new ManufacturerFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle ManufacturerListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _manufacturerRepository.Entities
               .Specify(manufacturerFilterSpec)
               .Select(m => new ManufacturerListDto
               {
                   Id = m.Id,
                   Name = m.Name,
                   City = m.City,
                   Country = m.Country
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _manufacturerRepository.Entities
            .Specify(manufacturerFilterSpec)
            .OrderBy(ordering)
            .Select(m => new ManufacturerListDto
            {
                Id = m.Id,
                Name = m.Name,
                City = m.City,
                Country = m.Country
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}