namespace maERP.Shared.Models;

public class Warehouse : ABaseModel
{
    public virtual string Name { get; set; } = String.Empty;

    public SalesChannel SalesChannels { get; set; }
}