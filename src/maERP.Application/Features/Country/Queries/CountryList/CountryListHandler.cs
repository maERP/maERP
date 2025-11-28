using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Country.Queries.CountryList;

public class CountryListHandler : IRequestHandler<CountryListQuery, PaginatedResult<CountryListDto>>
{
    private readonly IAppLogger<CountryListHandler> _logger;
    private readonly ICountryRepository _countryRepository;

    public CountryListHandler(
        IAppLogger<CountryListHandler> logger,
        ICountryRepository countryRepository)
    {
        _logger = logger;
        _countryRepository = countryRepository;
    }

    public async Task<PaginatedResult<CountryListDto>> Handle(CountryListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle CountryListQuery: {0}", request);

        var query = _countryRepository.Entities.AsNoTracking();

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            var searchLower = request.SearchString.ToLower();
            query = query.Where(c =>
                c.Name.ToLower().Contains(searchLower) ||
                c.CountryCode.ToLower().Contains(searchLower));
        }

        // Apply ordering
        if (request.OrderBy.Any())
        {
            var ordering = string.Join(",", request.OrderBy);
            query = query.OrderBy(ordering);
        }
        else
        {
            query = query.OrderBy(c => c.Name);
        }

        return await query
            .Select(c => new CountryListDto
            {
                Id = c.Id,
                Name = c.Name,
                CountryCode = c.CountryCode
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
