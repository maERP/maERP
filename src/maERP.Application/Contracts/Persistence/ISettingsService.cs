using maERP.Application.Models.Email;
using maERP.Application.Models.Grafana;
using maERP.Application.Models.Identity;
using maERP.Application.Models.Telemetry;

namespace maERP.Application.Contracts.Persistence;

public interface ISettingsService
{
    Task<JwtSettings> GetJwtSettingsAsync();
    Task<EmailSettings> GetEmailSettingsAsync();
    Task<TelemetrySettings> GetTelemetrySettingsAsync();
    Task<GrafanaSettings> GetGrafanaSettingsAsync();
    Task<string> GetSettingValueAsync(string key);
    Task SetSettingValueAsync(string key, string value);
}