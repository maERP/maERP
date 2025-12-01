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
    public const string OrderEdit = "OrderEdit";
    public const string OrderCreate = "OrderCreate";

    // Products
    public const string ProductList = "Products";
    public const string ProductDetail = "ProductDetail";
    public const string ProductEdit = "ProductEdit";
    public const string ProductCreate = "ProductCreate";

    // Manufacturers
    public const string ManufacturerList = "Manufacturers";
    public const string ManufacturerDetail = "ManufacturerDetail";
    public const string ManufacturerEdit = "ManufacturerEdit";

    // Inventory / Warehouses
    public const string WarehouseList = "Warehouses";
    public const string WarehouseDetail = "WarehouseDetail";
    public const string WarehouseEdit = "WarehouseEdit";
    public const string GoodsReceiptList = "GoodsReceipts";
    public const string GoodsReceiptDetail = "GoodsReceiptDetail";

    // Invoices
    public const string InvoiceList = "Invoices";
    public const string InvoiceDetail = "InvoiceDetail";

    // Sales Channels
    public const string SalesChannelList = "SalesChannels";
    public const string SalesChannelDetail = "SalesChannelDetail";
    public const string SalesChannelEdit = "SalesChannelEdit";

    // Settings
    public const string Settings = "Settings";
    public const string UserProfile = "UserProfile";
    public const string TenantSettings = "TenantSettings";

    // Tenants
    public const string TenantList = "Tenants";
    public const string TenantEdit = "TenantEdit";
    public const string TenantCreate = "TenantCreate";

    // AI Models
    public const string AiModelList = "AiModels";
    public const string AiModelDetail = "AiModelDetail";
    public const string AiModelEdit = "AiModelEdit";
    public const string AiModelCreate = "AiModelCreate";

    // AI Prompts
    public const string AiPromptList = "AiPrompts";
    public const string AiPromptDetail = "AiPromptDetail";
    public const string AiPromptEdit = "AiPromptEdit";
    public const string AiPromptCreate = "AiPromptCreate";

    // Tax Classes
    public const string TaxClassList = "TaxClasses";
    public const string TaxClassDetail = "TaxClassDetail";
    public const string TaxClassEdit = "TaxClassEdit";

    // Admin
    public const string AdminUsers = "AdminUsers";
    public const string AdminTenants = "AdminTenants";

    // Superadmin
    public const string SuperadminTenantList = "SuperadminTenants";
    public const string SuperadminTenantEdit = "SuperadminTenantEdit";

    // Legacy (for migration - remove after complete)
    public const string Main = "Main";
    public const string Second = "Second";
}