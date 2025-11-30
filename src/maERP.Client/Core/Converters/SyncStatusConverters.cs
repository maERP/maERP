using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts boolean sync status to localized display text (Enabled/Disabled).
/// </summary>
public class BoolToSyncStatusTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isEnabled)
        {
            return isEnabled
                ? GetLocalizedString("SyncStatus.Enabled")
                : GetLocalizedString("SyncStatus.Disabled");
        }
        return GetLocalizedString("SyncStatus.Disabled");
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

    private static string GetLocalizedString(string resourceKey)
    {
        try
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            var result = resourceLoader.GetString(resourceKey);
            return !string.IsNullOrEmpty(result) ? result : GetFallbackString(resourceKey);
        }
        catch
        {
            return GetFallbackString(resourceKey);
        }
    }

    private static string GetFallbackString(string resourceKey)
    {
        return resourceKey switch
        {
            "SyncStatus.Enabled" => "Enabled",
            "SyncStatus.Disabled" => "Disabled",
            _ => resourceKey
        };
    }
}

/// <summary>
/// Converts boolean sync status to background brush color.
/// </summary>
public class BoolToSyncStatusBackgroundConverter : IValueConverter
{
    private static readonly SolidColorBrush EnabledBrush = new(Color.FromArgb(255, 220, 252, 231)); // #DCFCE7 (green background)
    private static readonly SolidColorBrush DisabledBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 (gray background)

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isEnabled)
        {
            return isEnabled ? EnabledBrush : DisabledBrush;
        }
        return DisabledBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts boolean sync status to foreground brush color.
/// </summary>
public class BoolToSyncStatusForegroundConverter : IValueConverter
{
    private static readonly SolidColorBrush EnabledBrush = new(Color.FromArgb(255, 22, 163, 74)); // #16A34A (green text)
    private static readonly SolidColorBrush DisabledBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 (gray text)

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isEnabled)
        {
            return isEnabled ? EnabledBrush : DisabledBrush;
        }
        return DisabledBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
