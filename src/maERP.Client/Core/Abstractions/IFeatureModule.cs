namespace maERP.Client.Core.Abstractions;

/// <summary>
/// Interface for feature modules to implement consistent registration pattern.
/// Each feature module registers its own services, views, and routes.
/// </summary>
public interface IFeatureModule
{
    /// <summary>
    /// Registers services specific to this feature module.
    /// Called during app startup in ConfigureServices.
    /// </summary>
    static abstract IServiceCollection RegisterServices(IServiceCollection services);

    /// <summary>
    /// Registers views (ViewMaps) for this feature module.
    /// Called during route registration.
    /// </summary>
    static abstract void RegisterViews(IViewRegistry views);

    /// <summary>
    /// Gets the nested routes for this feature module.
    /// These are combined into the main route hierarchy.
    /// </summary>
    static abstract IEnumerable<RouteMap> GetRoutes(IViewRegistry views);
}
