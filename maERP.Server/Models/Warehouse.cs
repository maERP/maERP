namespace maERP.Server.Models;

public class Warehouse : BaseModel
{
    public string Name { get; set; } = String.Empty;

    public List<SalesChannel>? SalesChannels { get; set; }
}