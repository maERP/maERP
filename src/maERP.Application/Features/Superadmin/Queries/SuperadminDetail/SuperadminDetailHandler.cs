using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Superadmin.Queries.SuperadminDetail;

public class SuperadminDetailHandler : IRequestHandler<SuperadminDetailQuery, Result<SuperadminTenantDetailDto>>
{
    private readonly IAppLogger<SuperadminDetailHandler> _logger;
    private readonly ITenantRepository _tenantRepository;

    public SuperadminDetailHandler(
        IAppLogger<SuperadminDetailHandler> logger,
        ITenantRepository tenantRepository)
    {
        _logger = logger;
        _tenantRepository = tenantRepository;
    }

    public async Task<Result<SuperadminTenantDetailDto>> Handle(SuperadminDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SuperadminDetailHandler.Handle: Retrieving tenant with ID {request.Id}.");

        var tenant = await _tenantRepository.Entities
            .Include(t => t.UserTenants!)
                .ThenInclude(ut => ut.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (tenant == null)
        {
            _logger.LogWarning($"SuperadminDetailHandler.Handle: Tenant with ID {request.Id} not found.");
            return Result<SuperadminTenantDetailDto>.Fail(ResultStatusCode.NotFound, "Tenant not found.");
        }

        var users = tenant.UserTenants?
            .Where(ut => ut.User != null)
            .Select(ut => new SuperadminTenantUserDto
            {
                Id = ut.User!.Id,
                Email = ut.User.Email ?? string.Empty,
                Firstname = ut.User.Firstname,
                Lastname = ut.User.Lastname,
                IsDefault = ut.IsDefault,
                RoleManageTenant = ut.RoleManageTenant,
                RoleManageUser = ut.RoleManageUser,
                DateCreated = ut.User.DateCreated
            })
            .OrderBy(u => u.Lastname)
            .ThenBy(u => u.Firstname)
            .ToList() ?? new List<SuperadminTenantUserDto>();

        var tenantDetailDto = new SuperadminTenantDetailDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Description = tenant.Description,
            CompanyName = tenant.CompanyName,
            ContactEmail = tenant.ContactEmail,
            Phone = tenant.Phone,
            Website = tenant.Website,
            Street = tenant.Street,
            Street2 = tenant.Street2,
            PostalCode = tenant.PostalCode,
            City = tenant.City,
            State = tenant.State,
            Country = tenant.Country,
            Iban = tenant.Iban,
            DateCreated = tenant.DateCreated,
            DateModified = tenant.DateModified,
            UserCount = users.Count,
            Users = users
        };

        return Result<SuperadminTenantDetailDto>.Success(tenantDetailDto);
    }
}