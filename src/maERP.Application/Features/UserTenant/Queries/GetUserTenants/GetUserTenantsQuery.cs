using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Features.UserTenant.Queries.GetUserTenants;

public class GetUserTenantsQuery : IRequest<Result<List<UserTenantAssignmentDto>>>
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    public GetUserTenantsQuery(string userId)
    {
        UserId = userId;
    }
}