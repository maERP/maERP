namespace maERP.Client.Core.Constants;

/// <summary>
/// Centralized route names for navigation.
/// Using constants prevents typos and enables refactoring.
/// </summary>
public static class Routes
{
    // Shell
    public const string Shell = "";

    // Auth
    public const string Login = "Login";
    public const string ServerSelect = "ServerSelect";

    // Dashboard
    public const string Dashboard = "Dashboard";

    // Customers
    public const string CustomerList = "Customers";
    public const string CustomerDetail = "CustomerDetail";
    public const string CustomerEdit = "CustomerEdit";
    public const string CustomerCreate = "CustomerCreate";

    // Orders
    public const string OrderList = "Orders";
    public const string OrderDetail = "OrderDetail";
    public const string OrderCreate = "OrderCreate";

    // Products
    public const string ProductList = "Products";
    public const string ProductDetail = "ProductDetail";
    public const string ProductEdit = "ProductEdit";
    public const string ProductCreate = "ProductCreate";

    // Inventory / Warehouses
    public const string WarehouseList = "Warehouses";
    public const string WarehouseDetail = "WarehouseDetail";
    public const string GoodsReceiptList = "GoodsReceipts";
    public const string GoodsReceiptDetail = "GoodsReceiptDetail";

    // Invoices
    public const string InvoiceList = "Invoices";
    public const string InvoiceDetail = "InvoiceDetail";

    // Sales Channels
    public const string SalesChannelList = "SalesChannels";
    public const string SalesChannelDetail = "SalesChannelDetail";

    // Settings
    public const string Settings = "Settings";
    public const string UserProfile = "UserProfile";
    public const string TenantSettings = "TenantSettings";

    // Admin
    public const string AdminUsers = "AdminUsers";
    public const string AdminTenants = "AdminTenants";

    // Legacy (for migration - remove after complete)
    public const string Main = "Main";
    public const string Second = "Second";
}