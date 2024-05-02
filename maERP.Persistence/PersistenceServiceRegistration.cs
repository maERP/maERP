using maERP.Persistence.Configurations.Options;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace maERP.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();
        var dbOptions = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseOptions>>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(dbOptions.Value.DefaultConnection,
                b => b.MigrationsAssembly("maERP.Persistence"));
        });

        return services;
    }
}