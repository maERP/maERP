using Microsoft.Extensions.DependencyInjection;

namespace maERP.Analytics;

public static class AnalyticsServiceRegistration
{
    public static IServiceCollection AddAnalyticsServices(this IServiceCollection services, IServiceScopeFactory serviceScopeFactory)
    {
        return services;
    }
}