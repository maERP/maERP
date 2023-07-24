namespace maERP.Server.Models;

public class TaxClass : ABaseModel
{
    public double TaxRate { get; set; }

    public List<Product>? Products { get; set; }
}