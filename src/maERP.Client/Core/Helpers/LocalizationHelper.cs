namespace maERP.Client.Presentation;

/// <summary>
/// Helper to access localized strings from converters and other non-DI contexts.
/// Uses IStringLocalizer via the app's service provider to respect the current culture
/// set by Uno Extensions Localization.
/// </summary>
internal static class LocalizationHelper
{
    public static string GetLocalizedString(string resourceKey, string fallback)
    {
        try
        {
            var localizer = App.Current.Services.GetRequiredService<IStringLocalizer>();
            var result = localizer[resourceKey];
            return !result.ResourceNotFound ? result.Value : fallback;
        }
        catch
        {
            return fallback;
        }
    }
}
