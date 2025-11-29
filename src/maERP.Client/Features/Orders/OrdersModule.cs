using maERP.Client.Core.Constants;
using maERP.Client.Features.Orders.Models;
using maERP.Client.Features.Orders.Services;
using maERP.Client.Features.Orders.Views;

namespace maERP.Client.Features.Orders;

/// <summary>
/// Module registration for Orders feature.
/// Provides list and detail operations for order management.
/// </summary>
public static class OrdersModule
{
    /// <summary>
    /// Registers Orders services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // OrderService: Transient - stateless, creates new instance per request
        services.AddTransient<IOrderService, OrderService>();

        // Page models
        services.AddTransient<OrderListModel>();

        return services;
    }

    /// <summary>
    /// Registers Orders views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<OrderListPage, OrderListModel>()
        );
    }

    /// <summary>
    /// Gets the routes for the Orders feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.OrderList, View: views.FindByViewModel<OrderListModel>());
    }
}

/// <summary>
/// Navigation data for order detail page.
/// </summary>
public record OrderDetailData(Guid orderId);
