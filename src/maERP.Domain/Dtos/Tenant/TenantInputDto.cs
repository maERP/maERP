using System.ComponentModel.DataAnnotations;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.Tenant;

public class TenantInputDto : ITenantInputModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    [EmailAddress]
    [MaxLength(200)]
    public string ContactEmail { get; set; } = string.Empty;
}