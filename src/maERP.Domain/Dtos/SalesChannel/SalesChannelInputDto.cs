using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Enums;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.SalesChannel;

public class SalesChannelInputDto : ISalesChannelInputModel
{
    public int Id { get; set; }
    public SalesChannelType SalesChannelType { get; set; }
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
}

