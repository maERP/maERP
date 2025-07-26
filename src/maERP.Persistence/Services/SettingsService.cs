using maERP.Application.Contracts.Persistence;
using maERP.Application.Models.Email;
using maERP.Application.Models.Identity;
using maERP.Application.Models.Telemetry;
using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingRepository _settingRepository;

    public SettingsService(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }

    public Task<JwtSettings> GetJwtSettingsAsync()
    {
        var settings = _settingRepository.Entities.Where(s => s.Key.StartsWith("Jwt.")).ToList();
        
        var jwtSettings = new JwtSettings();
        
        foreach (var setting in settings)
        {
            switch (setting.Key)
            {
                case "Jwt.Key":
                    jwtSettings.Key = setting.Value;
                    break;
                case "Jwt.Issuer":
                    jwtSettings.Issuer = setting.Value;
                    break;
                case "Jwt.Audience":
                    jwtSettings.Audience = setting.Value;
                    break;
                case "Jwt.DurationInMinutes":
                    if (double.TryParse(setting.Value, out var duration))
                        jwtSettings.DurationInMinutes = duration;
                    break;
                case "Jwt.RefreshTokenExpireDays":
                    if (int.TryParse(setting.Value, out var refreshDays))
                        jwtSettings.RefreshTokenExpireDays = refreshDays;
                    break;
            }
        }

        return Task.FromResult(jwtSettings);
    }

    public Task<EmailSettings> GetEmailSettingsAsync()
    {
        var settings = _settingRepository.Entities.Where(s => s.Key.StartsWith("Email.")).ToList();
        
        var emailSettings = new EmailSettings();
        
        foreach (var setting in settings)
        {
            switch (setting.Key)
            {
                case "Email.ApiKey":
                    emailSettings.ApiKey = setting.Value;
                    break;
                case "Email.FromAddress":
                    emailSettings.FromAddress = setting.Value;
                    break;
                case "Email.FromName":
                    emailSettings.FromName = setting.Value;
                    break;
            }
        }

        return Task.FromResult(emailSettings);
    }

    public Task<TelemetrySettings> GetTelemetrySettingsAsync()
    {
        var settings = _settingRepository.Entities.Where(s => s.Key.StartsWith("Telemetry.")).ToList();
        
        var telemetrySettings = new TelemetrySettings();
        
        foreach (var setting in settings)
        {
            switch (setting.Key)
            {
                case "Telemetry.Endpoint":
                    telemetrySettings.Endpoint = setting.Value;
                    break;
                case "Telemetry.ServiceName":
                    telemetrySettings.ServiceName = setting.Value;
                    break;
            }
        }

        return Task.FromResult(telemetrySettings);
    }

    public Task<string> GetSettingValueAsync(string key)
    {
        var setting = _settingRepository.Entities.FirstOrDefault(s => s.Key == key);
        return Task.FromResult(setting?.Value ?? string.Empty);
    }

    public async Task SetSettingValueAsync(string key, string value)
    {
        var setting = _settingRepository.Entities.FirstOrDefault(s => s.Key == key);
        if (setting != null)
        {
            setting.Value = value;
            await _settingRepository.UpdateAsync(setting);
        }
        else
        {
            setting = new Setting { Key = key, Value = value };
            await _settingRepository.CreateAsync(setting);
        }
    }
}