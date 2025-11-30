using maERP.Client.Core.Constants;
using maERP.Client.Features.Products.Models;
using maERP.Client.Features.Products.Services;
using maERP.Client.Features.Products.Views;

namespace maERP.Client.Features.Products;

/// <summary>
/// Module registration for Products feature.
/// Provides list and detail operations for product management.
/// </summary>
public static class ProductsModule
{
    /// <summary>
    /// Registers Products services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // ProductService: Transient - stateless, creates new instance per request
        services.AddTransient<IProductService, ProductService>();

        // Page models
        services.AddTransient<ProductListModel>();
        services.AddTransient<ProductDetailModel>();
        services.AddTransient<ProductEditModel>();

        return services;
    }

    /// <summary>
    /// Registers Products views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<ProductListPage, ProductListModel>(),
            new ViewMap<ProductDetailPage, ProductDetailModel>(Data: new DataMap<ProductDetailData>()),
            new ViewMap<ProductEditPage, ProductEditModel>(Data: new DataMap<ProductEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the Products feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.ProductList, View: views.FindByViewModel<ProductListModel>());
        yield return new RouteMap(Routes.ProductDetail, View: views.FindByViewModel<ProductDetailModel>());
        yield return new RouteMap(Routes.ProductEdit, View: views.FindByViewModel<ProductEditModel>());
        yield return new RouteMap(Routes.ProductCreate, View: views.FindByViewModel<ProductEditModel>());
    }
}
