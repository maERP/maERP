using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class SalesChannel : ABaseModel
{
    public virtual SalesChannelType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual string URL { get; set; } = string.Empty;
    public virtual string Username { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    public virtual bool ImportProducts { get; set; }
    public virtual bool ImportCustomers { get; set; }
    public virtual bool ImportOrders { get; set; }
    public virtual bool ExportProducts { get; set; }
    public virtual bool ExportCustomers { get; set; }
    public virtual bool ExportOrders { get; set; }
    public virtual Warehouse Warehouse { get; set; } = new();
}