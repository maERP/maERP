using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class TaxClass : BaseEntity, IBaseEntity
{
    public double TaxRate { get; set; }
    public List<Product>? Products { get; set; }
}