using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Queries.SuperadminDetail;

public class SuperadminDetailHandler : IRequestHandler<SuperadminDetailQuery, Result<TenantDetailDto>>
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

    public async Task<Result<TenantDetailDto>> Handle(SuperadminDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SuperadminDetailHandler.Handle: Retrieving tenant with ID {request.Id}.");

        var tenant = await _tenantRepository.GetByIdAsync(request.Id);

        if (tenant == null)
        {
            _logger.LogWarning($"SuperadminDetailHandler.Handle: Tenant with ID {request.Id} not found.");
            return Result<TenantDetailDto>.Fail(ResultStatusCode.NotFound, "Tenant not found.");
        }

        var tenantDetailDto = new TenantDetailDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            TenantCode = tenant.TenantCode,
            Description = tenant.Description,
            IsActive = tenant.IsActive,
            ContactEmail = tenant.ContactEmail,
            DateCreated = tenant.DateCreated,
            DateModified = tenant.DateModified,
            UserCount = tenant.UserTenants?.Count ?? 0
        };

        return Result<TenantDetailDto>.Success(tenantDetailDto);
    }
}