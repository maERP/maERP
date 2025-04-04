using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class SalesChannel : BaseEntity, IBaseEntity
{
    public SalesChannelType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool ImportProducts { get; set; }
    public bool ImportCustomers { get; set; }
    public bool ImportOrders { get; set; }
    public bool ExportProducts { get; set; }
    public bool ExportCustomers { get; set; }
    public bool ExportOrders { get; set; }
    public bool InitialProductImportCompleted { get; set; }
    public bool InitialProductExportCompleted { get; set; }

    // public Warehouse Warehouse { get; set; } = new();
    public int WarehouseId { get; set; }
}