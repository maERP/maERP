using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Dtos.SalesChannel;
 
public class SalesChannelCreateDto
{
    [Required]
    public int Type { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, Url]
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
}