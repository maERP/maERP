using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace maERP.UI.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsPath;
    private const string SettingsFileName = "maerp-settings.json";

    public SettingsService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appFolder = Path.Combine(appDataPath, "maERP");
        Directory.CreateDirectory(appFolder);
        _settingsPath = Path.Combine(appFolder, SettingsFileName);
    }

    public async Task SaveLoginCredentialsAsync(string email, string password, string serverUrl)
    {
        var settings = await LoadSettingsAsync() ?? new AppSettings();

        settings.SavedCredentials = new SavedCredentials
        {
            Email = email,
            Password = EncodePassword(password),
            ServerUrl = serverUrl,
            IsEncrypted = true
        };

        await SaveSettingsAsync(settings);
    }

    public async Task<(string Email, string Password, string ServerUrl)?> LoadLoginCredentialsAsync()
    {
        var settings = await LoadSettingsAsync();
        if (settings?.SavedCredentials == null || !settings.RememberMe)
            return null;

        var credentials = settings.SavedCredentials;
        var password = credentials.IsEncrypted
            ? DecodePassword(credentials.Password)
            : credentials.Password;

        return (credentials.Email, password, credentials.ServerUrl);
    }

    public async Task ClearLoginCredentialsAsync()
    {
        var settings = await LoadSettingsAsync() ?? new AppSettings();
        settings.SavedCredentials = null;
        await SaveSettingsAsync(settings);
    }

    public async Task<bool> GetRememberMeAsync()
    {
        var settings = await LoadSettingsAsync();
        return settings?.RememberMe ?? false;
    }

    public async Task SetRememberMeAsync(bool rememberMe)
    {
        var settings = await LoadSettingsAsync() ?? new AppSettings();
        settings.RememberMe = rememberMe;

        if (!rememberMe)
        {
            settings.SavedCredentials = null;
        }

        await SaveSettingsAsync(settings);
    }

    private async Task<AppSettings?> LoadSettingsAsync()
    {
        try
        {
            if (!File.Exists(_settingsPath))
                return null;

            var json = await File.ReadAllTextAsync(_settingsPath);
            return JsonSerializer.Deserialize<AppSettings>(json);
        }
        catch
        {
            return null;
        }
    }

    private async Task SaveSettingsAsync(AppSettings settings)
    {
        try
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            await File.WriteAllTextAsync(_settingsPath, json);
        }
        catch
        {
            // Silent fail - settings won't be persisted but app continues to work
        }
    }

    private static string EncodePassword(string password)
    {
        // Einfache Base64-Kodierung als Basis-Obfuskierung
        // Hinweis: Das ist keine echte Sicherheit, aber besser als Klartext
        var bytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(bytes);
    }

    private static string DecodePassword(string encodedPassword)
    {
        try
        {
            var bytes = Convert.FromBase64String(encodedPassword);
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            // Fallback: Rückgabe als Klartext falls Dekodierung fehlschlägt
            return encodedPassword;
        }
    }

    private class AppSettings
    {
        public bool RememberMe { get; set; }
        public SavedCredentials? SavedCredentials { get; set; }
    }

    private class SavedCredentials
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ServerUrl { get; set; } = string.Empty;
        public bool IsEncrypted { get; set; }
    }
}