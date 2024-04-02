using maERP.Domain.Common;

namespace maERP.Domain;

public class TaxClass : BaseEntity
{
    public double TaxRate { get; set; }

    public List<Product>? Products { get; set; }
}