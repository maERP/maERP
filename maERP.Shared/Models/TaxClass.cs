namespace maERP.Shared.Models;

public class TaxClass : ABaseModel
{
    public double TaxRate { get; set; }

    public Product? Product { get; set; }
}