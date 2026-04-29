namespace maERP.Application.Models.Grafana;

public class GrafanaSettings
{
    public string Endpoint { get; set; } = string.Empty;
    public string LokiEndpoint { get; set; } = string.Empty;
    public bool MetricsEnabled { get; set; }
    public bool LogsEnabled { get; set; }
}
