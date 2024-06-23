using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class Country : BaseEntity, IBaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string CountryCode { get; set; } = null!;
}