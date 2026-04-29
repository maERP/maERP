using maERP.Application.Models.Grafana;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace maERP.Server.ServiceRegistrations;

public static class OpenTelemetryRegistration
{
    public static IServiceCollection AddGrafanaTelemetryServices(
        this IServiceCollection services,
        GrafanaSettings grafanaSettings,
        string serviceName)
    {
        if (!grafanaSettings.MetricsEnabled)
        {
            return services;
        }

        if (!Uri.TryCreate(grafanaSettings.Endpoint, UriKind.Absolute, out var metricsUri))
        {
            return services;
        }

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName)
                .AddEnvironmentVariableDetector())
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = metricsUri;
                    options.Protocol = OtlpExportProtocol.HttpProtobuf;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = metricsUri;
                    options.Protocol = OtlpExportProtocol.HttpProtobuf;
                }));

        return services;
    }
}
