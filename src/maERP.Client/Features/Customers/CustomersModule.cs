using maERP.Client.Core.Constants;
using maERP.Client.Features.Countries.Services;
using maERP.Client.Features.Customers.Models;
using maERP.Client.Features.Customers.Services;
using maERP.Client.Features.Customers.Views;

namespace maERP.Client.Features.Customers;

/// <summary>
/// Module registration for Customers feature.
/// Provides CRUD operations for customer management.
/// </summary>
public static class CustomersModule
{
    /// <summary>
    /// Registers Customers services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // CustomerService: Transient - stateless, creates new instance per request
        services.AddTransient<ICustomerService, CustomerService>();

        // CountryService: Singleton - maintains in-memory cache for country lookups
        services.AddSingleton<ICountryService, CountryService>();

        // Page models
        services.AddTransient<CustomerListModel>();
        services.AddTransient<CustomerDetailModel>();
        services.AddTransient<CustomerEditModel>();

        return services;
    }

    /// <summary>
    /// Registers Customers views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<CustomerListPage, CustomerListModel>(),
            new ViewMap<CustomerDetailPage, CustomerDetailModel>(Data: new DataMap<CustomerDetailData>()),
            new ViewMap<CustomerEditPage, CustomerEditModel>(Data: new DataMap<CustomerEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the Customers feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.CustomerList, View: views.FindByViewModel<CustomerListModel>());
        yield return new RouteMap(Routes.CustomerDetail, View: views.FindByViewModel<CustomerDetailModel>());
        yield return new RouteMap(Routes.CustomerEdit, View: views.FindByViewModel<CustomerEditModel>());
        yield return new RouteMap(Routes.CustomerCreate, View: views.FindByViewModel<CustomerEditModel>());
    }
}

/// <summary>
/// Navigation data for customer detail page.
/// </summary>
public record CustomerDetailData(Guid customerId);

/// <summary>
/// Navigation data for customer edit page.
/// </summary>
public record CustomerEditData(Guid customerId);
