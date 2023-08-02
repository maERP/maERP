using maERP.Shared.Dtos.Warehouse;
using System.ComponentModel.DataAnnotations;


namespace maERP.Shared.Dtos.SalesChannel;
 
public class SalesChannelDetailDto
{
    public int Id { get; set; }
    public int Type { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool ImportProducts { get; set; }
    public bool ExportProducts { get; set; }
    public bool ImportCustomers { get; set; }
    public bool ExportCustomers { get; set; }
    public bool ImportOrders { get; set; }
    public bool ExportOrders { get; set; }

    public int WarehouseId { get; set; }
    public WarehouseDetailDto? Warehouse { get; set; }
}