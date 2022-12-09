#nullable disable

namespace maERP.Shared.Dtos;

public class SalesChannelBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }

    public bool ImportProducts { get; set; }
    public bool ImportCustomers { get; set; }
    public bool ImportOrders { get; set; }
    public bool ExportProducts { get; set; }
    public bool ExportCustomers { get; set; }
    public bool ExportOrders { get; set; }
}