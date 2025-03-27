using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace maERP.Server.ServiceRegistrations;

public static class OpenTelemetryRegistration
{
    public static IServiceCollection AddOpenTelemetryServices(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {
        var telemetryEndpoint = configuration["Telemetry:Endpoint"] ?? "http://localhost:4317";
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName)
                .AddEnvironmentVariableDetector())
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(telemetryEndpoint);
                    options.Protocol = OtlpExportProtocol.Grpc;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(telemetryEndpoint);
                    options.Protocol = OtlpExportProtocol.Grpc;
                }));

        return services;
    }
}