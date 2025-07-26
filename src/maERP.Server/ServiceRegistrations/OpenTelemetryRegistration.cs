using maERP.Application.Models.Telemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace maERP.Server.ServiceRegistrations;

public static class OpenTelemetryRegistration
{
    public static IServiceCollection AddOpenTelemetryServices(this IServiceCollection services, TelemetrySettings telemetrySettings)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(telemetrySettings.ServiceName)
                .AddEnvironmentVariableDetector())
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(telemetrySettings.Endpoint);
                    options.Protocol = OtlpExportProtocol.Grpc;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(telemetrySettings.Endpoint);
                    options.Protocol = OtlpExportProtocol.Grpc;
                }));

        return services;
    }

    public static IServiceCollection AddOpenTelemetryServices(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {
        var telemetryEndpoint = configuration["Telemetry:Endpoint"] ?? "http://localhost:4317";
        
        var telemetrySettings = new TelemetrySettings
        {
            Endpoint = telemetryEndpoint,
            ServiceName = serviceName
        };
        
        return services.AddOpenTelemetryServices(telemetrySettings);
    }
}