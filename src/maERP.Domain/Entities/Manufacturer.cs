using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class Manufacturer : BaseEntity, IBaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Street { get; set; }
    public string City { get; set; } = string.Empty;
    public string? State { get; set; }
    public string Country { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Logo { get; set; }
}