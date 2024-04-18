using maERP.Persistence.DatabaseContext;
using maERP.Server.Configurations.Options;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace maERP.Server.ServiceRegistrations;

public static class DatabaseRegistration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IOptions<DatabaseOptions> dbOptions)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(dbOptions.Value.DefaultConnection);
        });
        
        return services;
    }
}
