using Microsoft.Extensions.DependencyInjection;

namespace maERP.AI;

public static class AIServiceRegistration
{
    public static IServiceCollection AddAIServices(this IServiceCollection services, IServiceScopeFactory serviceScopeFactory)
    {
        return services;
    }
}