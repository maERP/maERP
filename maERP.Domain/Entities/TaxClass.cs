using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class TaxClass : BaseEntity, IBaseEntity
{
    public double TaxRate { get; set; }
    public List<Product>? Products { get; set; }
}