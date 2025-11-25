using maERP.Client.Core.Constants;
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
        // Feature-specific service
        services.AddTransient<ICustomerService, CustomerService>();

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
            new ViewMap<CustomerDetailPage, CustomerDetailModel>(),
            new ViewMap<CustomerEditPage, CustomerEditModel>()
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
