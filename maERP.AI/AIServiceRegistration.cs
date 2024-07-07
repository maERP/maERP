using Microsoft.Extensions.DependencyInjection;

namespace maERP.Ai;

public static class AiServiceRegistration
{
    public static IServiceCollection AddAIServices(this IServiceCollection services, IServiceScopeFactory serviceScopeFactory)
    {
        return services;
    }
}