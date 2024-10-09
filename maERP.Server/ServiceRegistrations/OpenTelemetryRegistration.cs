using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace maERP.Server.ServiceRegistrations;

public static class OpenTelemetryRegistration
{
    public static IServiceCollection AddOpenTelemetryServices(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {
        services.AddOpenTelemetry()
       .WithTracing(tracing => tracing
           .AddAspNetCoreInstrumentation()
           .AddOtlpExporter(options =>
           {
               options.Endpoint = new Uri("http://maerp.de:4317");
               options.Protocol = OtlpExportProtocol.Grpc;
           }))
       .WithMetrics(metrics => metrics
           .AddAspNetCoreInstrumentation()
           .AddOtlpExporter(options =>
           {
               options.Endpoint = new Uri("http://maerp.de:4317");
               options.Protocol = OtlpExportProtocol.Grpc;
           }));

        return services;
    }
}