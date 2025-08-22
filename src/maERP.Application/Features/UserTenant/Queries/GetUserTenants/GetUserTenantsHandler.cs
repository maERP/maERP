using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.UserTenant.Queries.GetUserTenants;

public class GetUserTenantsHandler : IRequestHandler<GetUserTenantsQuery, Result<List<UserTenantAssignmentDto>>>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly IValidator<GetUserTenantsQuery> _validator;

    public GetUserTenantsHandler(
        IUserTenantRepository userTenantRepository,
        IValidator<GetUserTenantsQuery> validator)
    {
        _userTenantRepository = userTenantRepository;
        _validator = validator;
    }

    public async Task<Result<List<UserTenantAssignmentDto>>> Handle(GetUserTenantsQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await Result<List<UserTenantAssignmentDto>>.FailAsync(validationResult.ToString());
        }

        var userTenants = await _userTenantRepository.Entities
            .Where(ut => ut.UserId == request.UserId)
            .Include(ut => ut.Tenant)
            .Select(ut => new UserTenantAssignmentDto
            {
                TenantId = ut.TenantId,
                TenantName = ut.Tenant!.Name,
                TenantCode = ut.Tenant.TenantCode,
                IsDefault = ut.IsDefault
            })
            .ToListAsync(cancellationToken);

        return await Result<List<UserTenantAssignmentDto>>.SuccessAsync(userTenants);
    }
}