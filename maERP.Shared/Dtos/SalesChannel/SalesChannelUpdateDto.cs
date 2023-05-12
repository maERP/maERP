using System.Text.Json.Serialization;

namespace maERP.Shared.Dtos;
 
public class SalesChannelUpdateDto
{
    [JsonIgnore]
    public virtual uint Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Url { get; set; } = string.Empty;
    public virtual string Username { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    public virtual bool ImportProducts { get; set; }
    public virtual bool ExportProducts { get; set; }
    public virtual bool ImportCustomers { get; set; }
    public virtual bool ExportCustomers { get; set; }
    public virtual bool ImportOrders { get; set; }
    public virtual bool ExportOrders { get; set; }
}