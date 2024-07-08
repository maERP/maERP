using maERP.Ai.Services;
using maERP.Application.Contracts.Ai;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Ai;

public static class AiServiceRegistration
{
    public static IServiceCollection AddAiServices(this IServiceCollection services)
    {
        services.AddScoped<IAiServiceWrapper, AiServiceWrapper>();

        return services;
    }
}