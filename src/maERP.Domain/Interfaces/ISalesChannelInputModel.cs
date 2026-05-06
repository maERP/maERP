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
    bool ImportSaless { get; }
    bool ExportProducts { get; }
    bool ExportCustomers { get; }
    bool ExportSaless { get; }
    bool InitialProductImportCompleted { get; }
    bool InitialProductExportCompleted { get; }
    List<Guid> WarehouseIds { get; }
}
