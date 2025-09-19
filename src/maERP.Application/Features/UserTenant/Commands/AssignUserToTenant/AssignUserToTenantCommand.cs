using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Features.UserTenant.Commands.AssignUserToTenant;

public class AssignUserToTenantCommand : IRequest<Result<int>>
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public Guid TenantId { get; set; }

    public bool IsDefault { get; set; } = false;

    public bool RoleManageUser { get; set; } = false;
}
