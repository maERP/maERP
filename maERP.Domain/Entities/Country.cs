using System.ComponentModel.DataAnnotations;
using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class Country : BaseEntity, IBaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string CountryCode { get; set; } = null!;
}