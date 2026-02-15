using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Queries.SuperadminList;

public class SuperadminListHandler : IRequestHandler<SuperadminListQuery, PaginatedResult<SuperadminTenantListDto>>
{
    private readonly IAppLogger<SuperadminListHandler> _logger;
    private readonly ITenantRepository _tenantRepository;

    public SuperadminListHandler(
        IAppLogger<SuperadminListHandler> logger,
        ITenantRepository tenantRepository)
    {
        _logger = logger;
        _tenantRepository = tenantRepository;
    }

    public async Task<PaginatedResult<SuperadminTenantListDto>> Handle(SuperadminListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SuperadminListHandler.Handle: Retrieving tenants.");

        var query = _tenantRepository.Entities.AsQueryable();

        // Apply search filter
        if (!string.IsNullOrEmpty(request.SearchString))
        {
            query = query.Where(t => t.Name.Contains(request.SearchString) ||
                                   t.Description.Contains(request.SearchString) ||
                                   (t.CompanyName != null && t.CompanyName.Contains(request.SearchString)) ||
                                   (t.ContactEmail != null && t.ContactEmail.Contains(request.SearchString)) ||
                                   (t.Phone != null && t.Phone.Contains(request.SearchString)) ||
                                   (t.City != null && t.City.Contains(request.SearchString)) ||
                                   (t.Country != null && t.Country.Contains(request.SearchString)));
        }

        // Apply ordering
        if (request.OrderBy.Any())
        {
            var ordering = string.Join(",", request.OrderBy);
            query = query.OrderBy(ordering);
        }
        else
        {
            query = query.OrderBy(t => t.Name);
        }

        return await query
            .Select(t => new SuperadminTenantListDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                CompanyName = t.CompanyName,
                ContactEmail = t.ContactEmail,
                Phone = t.Phone,
                Website = t.Website,
                Street = t.Street,
                Street2 = t.Street2,
                PostalCode = t.PostalCode,
                City = t.City,
                State = t.State,
                Country = t.Country,
                Iban = t.Iban,
                DateCreated = t.DateCreated,
                DateModified = t.DateModified
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
