using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using maERP.Application.Contracts.Identity;
using maERP.Application.Mediator;
using maERP.Application.Services.Identity;
using FluentValidation;

namespace maERP.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register custom mediator
        services.AddScoped<IMediator, CustomMediator>();

        // Register services
        services.AddScoped<IUserTenantService, UserTenantService>();

        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register all handlers
        RegisterHandlers(services);

        return services;
    }

    private static void RegisterHandlers(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Find all handler types
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToArray();

        // Register each handler
        foreach (var handlerType in handlerTypes)
        {
            var handlerInterfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            foreach (var handlerInterface in handlerInterfaces)
            {
                services.AddScoped(handlerInterface, handlerType);
            }
        }
    }
}