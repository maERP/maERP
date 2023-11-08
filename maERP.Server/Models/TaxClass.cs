namespace maERP.Server.Models;

public class TaxClass : BaseModel
{
    public double TaxRate { get; set; }

    public List<Product>? Products { get; set; }
}