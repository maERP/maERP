using System.Reflection;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace maERP.Server.ServiceRegistrations;

public static class ApiVersioningRegistration
{
    public static IServiceCollection AddApiVersioningServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("ver")
            );
        });

        services.AddVersionedApiExplorer(
        options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}