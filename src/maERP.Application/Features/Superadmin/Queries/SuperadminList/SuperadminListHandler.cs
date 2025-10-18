using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Queries.SuperadminList;

public class SuperadminListHandler : IRequestHandler<SuperadminListQuery, PaginatedResult<TenantListDto>>
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

    public async Task<PaginatedResult<TenantListDto>> Handle(SuperadminListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SuperadminListHandler.Handle: Retrieving tenants.");

        var query = _tenantRepository.Entities.AsQueryable();

        // Apply search filter
        if (!string.IsNullOrEmpty(request.SearchString))
        {
            query = query.Where(t => t.Name.Contains(request.SearchString) ||
                                   t.Description.Contains(request.SearchString) ||
                                   (t.ContactEmail != null && t.ContactEmail.Contains(request.SearchString)));
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
            .Select(t => new TenantListDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                IsActive = t.IsActive,
                ContactEmail = t.ContactEmail,
                DateCreated = t.DateCreated,
                DateModified = t.DateModified
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}