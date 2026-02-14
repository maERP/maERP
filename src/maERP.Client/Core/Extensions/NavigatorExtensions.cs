using maERP.Client.Core.Constants;

namespace maERP.Client.Core.Extensions;

/// <summary>
/// Extension methods for INavigator to provide type-safe navigation.
/// </summary>
public static class NavigatorExtensions
{
    /// <summary>
    /// Navigate to customer detail page with customer ID.
    /// </summary>
    public static async ValueTask<NavigationResponse?> NavigateToCustomerDetailAsync(
        this INavigator navigator,
        object sender,
        Guid customerId,
        CancellationToken ct = default)
    {
        return await navigator.NavigateRouteAsync(
            sender,
            Routes.CustomerDetail,
            data: new Dictionary<string, object> { ["Id"] = customerId },
            cancellation: ct);
    }

    /// <summary>
    /// Navigate to order detail page with order ID.
    /// </summary>
    public static async ValueTask<NavigationResponse?> NavigateToOrderDetailAsync(
        this INavigator navigator,
        object sender,
        Guid orderId,
        CancellationToken ct = default)
    {
        return await navigator.NavigateRouteAsync(
            sender,
            Routes.OrderDetail,
            data: new Dictionary<string, object> { ["Id"] = orderId },
            cancellation: ct);
    }

    /// <summary>
    /// Navigate to product detail page with product ID.
    /// </summary>
    public static async ValueTask<NavigationResponse?> NavigateToProductDetailAsync(
        this INavigator navigator,
        object sender,
        Guid productId,
        CancellationToken ct = default)
    {
        return await navigator.NavigateRouteAsync(
            sender,
            Routes.ProductDetail,
            data: new Dictionary<string, object> { ["Id"] = productId },
            cancellation: ct);
    }

    /// <summary>
    /// Navigate to dashboard (main page after login).
    /// </summary>
    public static async ValueTask<NavigationResponse?> NavigateToDashboardAsync(
        this INavigator navigator,
        object sender,
        CancellationToken ct = default)
    {
        return await navigator.NavigateRouteAsync(
            sender,
            Routes.Dashboard,
            qualifier: Qualifiers.ClearBackStack,
            cancellation: ct);
    }

    /// <summary>
    /// Navigate back if possible.
    /// </summary>
    public static async ValueTask<NavigationResponse?> GoBackSafeAsync(
        this INavigator navigator,
        object sender,
        CancellationToken ct = default)
    {
        return await navigator.NavigateBackAsync(sender, cancellation: ct);
    }
}
