namespace maERP.Domain.Enums;

/// <summary>
/// Who fulfills a per-channel listing. Drives whether the ERP pushes stock or treats it as
/// remote-managed (e.g. Amazon FBA — Amazon controls the inventory).
/// </summary>
public enum FulfillmentChannel
{
    Merchant = 0,
    AmazonFBA = 1
}
