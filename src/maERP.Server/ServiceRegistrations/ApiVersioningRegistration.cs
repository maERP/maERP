using Asp.Versioning;

namespace maERP.Server.ServiceRegistrations;

public static class ApiVersioningRegistration
{
    public static IServiceCollection AddApiVersioningServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc()
        .AddApiExplorer(x =>
        {
            x.GroupNameFormat = "'v'VVV";
            x.SubstituteApiVersionInUrl = true;
            x.ApiVersionParameterSource = new UrlSegmentApiVersionReader();
        });


        return services;
    }
}