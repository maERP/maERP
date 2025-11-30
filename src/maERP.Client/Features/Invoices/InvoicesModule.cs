using maERP.Client.Core.Constants;
using maERP.Client.Features.Invoices.Models;
using maERP.Client.Features.Invoices.Services;
using maERP.Client.Features.Invoices.Views;

namespace maERP.Client.Features.Invoices;

/// <summary>
/// Module registration for Invoices feature.
/// Provides listing and viewing of invoices.
/// </summary>
public static class InvoicesModule
{
    /// <summary>
    /// Registers Invoices services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // InvoiceService: Transient - stateless, creates new instance per request
        services.AddTransient<IInvoiceService, InvoiceService>();

        // Page models
        services.AddTransient<InvoiceListModel>();
        services.AddTransient<InvoiceDetailModel>();

        return services;
    }

    /// <summary>
    /// Registers Invoices views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<InvoiceListPage, InvoiceListModel>(),
            new ViewMap<InvoiceDetailPage, InvoiceDetailModel>(Data: new DataMap<InvoiceDetailData>())
        );
    }

    /// <summary>
    /// Gets the routes for the Invoices feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.InvoiceList, View: views.FindByViewModel<InvoiceListModel>());
        yield return new RouteMap(Routes.InvoiceDetail, View: views.FindByViewModel<InvoiceDetailModel>());
    }
}
