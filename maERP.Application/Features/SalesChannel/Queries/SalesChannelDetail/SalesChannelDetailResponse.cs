﻿using maERP.Application.Features.Warehouse.Queries.WarehouseDetail;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

public class SalesChannelDetailResponse
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
    public WarehouseDetailResponse? Warehouse { get; set; }
}