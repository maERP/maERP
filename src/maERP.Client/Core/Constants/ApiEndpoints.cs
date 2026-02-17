namespace maERP.Client.Core.Constants;

/// <summary>
/// Centralized API endpoint paths.
/// Base URL is configured via appsettings.json or during login.
/// </summary>
public static class ApiEndpoints
{
    public const string ApiVersion = "v1";
    public const string ApiBase = $"/api/{ApiVersion}";

    // Auth
    public static class Auth
    {
        public const string Base = $"{ApiBase}/auth";
        public const string Login = $"{Base}/login";
        public const string Register = $"{Base}/register";
        public const string RefreshToken = $"{Base}/refresh";
    }

    // AI Models
    public static class AiModels
    {
        public const string Base = $"{ApiBase}/aimodels";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // AI Prompts
    public static class AiPrompts
    {
        public const string Base = $"{ApiBase}/aiprompts";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Countries
    public static class Countries
    {
        public const string Base = $"{ApiBase}/countries";
    }

    // Customers
    public static class Customers
    {
        public const string Base = $"{ApiBase}/customers";
        public const string Search = $"{Base}/search";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Orders
    public static class Orders
    {
        public const string Base = $"{ApiBase}/orders";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Products
    public static class Products
    {
        public const string Base = $"{ApiBase}/products";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Warehouses
    public static class Warehouses
    {
        public const string Base = $"{ApiBase}/warehouses";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Goods Receipts
    public static class GoodsReceipts
    {
        public const string Base = $"{ApiBase}/goodsreceipts";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Invoices
    public static class Invoices
    {
        public const string Base = $"{ApiBase}/invoices";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Sales Channels
    public static class SalesChannels
    {
        public const string Base = $"{ApiBase}/saleschannels";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Manufacturers
    public static class Manufacturers
    {
        public const string Base = $"{ApiBase}/manufacturers";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Tax Classes
    public static class TaxClasses
    {
        public const string Base = $"{ApiBase}/taxclasses";
        public static string ById(Guid id) => $"{Base}/{id}";
    }

    // Settings
    public static class Settings
    {
        public const string Base = $"{ApiBase}/settings";
    }

    // Statistics
    public static class Statistics
    {
        public const string Base = $"{ApiBase}/statistics";
        public const string SalesToday = $"{Base}/SalesToday";
        public const string OrdersToday = $"{Base}/OrdersToday";
        public const string CustomersToday = $"{Base}/CustomersToday";
        public const string ProductsToday = $"{Base}/ProductsToday";
        public const string OrdersLatest = $"{Base}/OrdersLatest";
        public const string ProductsBestSelling = $"{Base}/ProductsBestSelling";
    }

    // Tenants
    public static class Tenants
    {
        public const string Base = $"{ApiBase}/tenants";
        public static string ById(Guid id) => $"{Base}/{id}";
        public static string UserSearch(Guid tenantId) => $"{Base}/{tenantId}/users/search";
        public static string Users(Guid tenantId) => $"{Base}/{tenantId}/users";
    }

    // Users
    public static class Users
    {
        public const string Base = $"{ApiBase}/users";
        public static string ById(string id) => $"{Base}/{id}";
        public const string Me = $"{Base}/me";
    }

    // Superadmin
    public static class Superadmin
    {
        public const string Base = $"{ApiBase}/superadmin";
        public const string Users = $"{Base}/users";
        public static string UserById(string id) => $"{Users}/{id}";
        public const string Tenants = $"{Base}/tenants";
        public static string TenantById(Guid id) => $"{Tenants}/{id}";

        // User-Tenant management endpoints
        public static string UserTenants(string userId) => $"{Users}/{userId}/tenants";
        public static string UserTenantById(string userId, Guid tenantId) => $"{Users}/{userId}/tenants/{tenantId}";
    }
}