using maERP.Domain.Enums;

namespace maERP.Domain.Interfaces;

public interface ISalesChannelInputModel
{
    SalesChannelType SalesChannelType { get; }
    string Name { get; }
    string Url { get; }
    string Username { get; }
    string Password { get; }
    bool ImportProducts { get; }
    bool ImportCustomers { get; }
    bool ImportOrders { get; }
    bool ExportProducts { get; }
    bool ExportCustomers { get; }
    bool ExportOrders { get; }
    List<Guid> WarehouseIds { get; }
}
