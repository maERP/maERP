using maERP.Domain.Entities;
using maERP.Persistence.Configurations.Options;
using maERP.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace maERP.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();
        var dbOptions = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
        var connectionString = dbOptions.GetConnectionString();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

            switch (dbOptions.Provider.ToUpperInvariant())
            {
                case "MYSQL":
                    options.UseMySql(connectionString, 
                        ServerVersion.AutoDetect(connectionString),
                        b => b.MigrationsAssembly("maERP.Persistence.MySQL"));
                    break;

                case "MSSQL":
                    options.UseSqlServer(connectionString,
                        b => b.MigrationsAssembly("maERP.Persistence.MSSQL"));
                    break;

                case "POSTGRESQL":
                    options.UseNpgsql(connectionString,
                        b => b.MigrationsAssembly("maERP.Persistence.PostgreSQL"));
                    break;

                default:
                    throw new ArgumentException($"Unsupported database provider: {dbOptions.Provider}");
            }
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}