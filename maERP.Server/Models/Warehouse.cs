namespace maERP.Server.Models;

public class Warehouse : ABaseModel
{
    public string Name { get; set; } = String.Empty;

    public List<SalesChannel>? SalesChannels { get; set; }
}