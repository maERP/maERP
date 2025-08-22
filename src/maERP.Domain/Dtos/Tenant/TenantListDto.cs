using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Tenant;

public class TenantListDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string TenantCode { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public string ContactEmail { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}