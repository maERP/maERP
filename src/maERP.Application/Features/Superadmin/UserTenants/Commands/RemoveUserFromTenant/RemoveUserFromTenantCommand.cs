using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Features.Superadmin.UserTenants.Commands.RemoveUserFromTenant;

public class RemoveUserFromTenantCommand : IRequest<Result<bool>>
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public Guid TenantId { get; set; }
}