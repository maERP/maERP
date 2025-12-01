using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts boolean tenant active status to localized display text (Active/Inactive).
/// </summary>
public class BoolToStatusTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isActive)
        {
            return isActive
                ? GetLocalizedString("TenantStatus.Active")
                : GetLocalizedString("TenantStatus.Inactive");
        }
        return GetLocalizedString("TenantStatus.Inactive");
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
            "TenantStatus.Active" => "Active",
            "TenantStatus.Inactive" => "Inactive",
            _ => resourceKey
        };
    }
}

/// <summary>
/// Converts boolean tenant active status to background brush color.
/// </summary>
public class BoolToStatusBackgroundConverter : IValueConverter
{
    private static readonly SolidColorBrush ActiveBrush = new(Color.FromArgb(255, 220, 252, 231)); // #DCFCE7 (green background)
    private static readonly SolidColorBrush InactiveBrush = new(Color.FromArgb(255, 254, 226, 226)); // #FEE2E2 (red background)

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isActive)
        {
            return isActive ? ActiveBrush : InactiveBrush;
        }
        return InactiveBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts boolean tenant active status to foreground brush color.
/// </summary>
public class BoolToStatusForegroundConverter : IValueConverter
{
    private static readonly SolidColorBrush ActiveBrush = new(Color.FromArgb(255, 22, 163, 74)); // #16A34A (green text)
    private static readonly SolidColorBrush InactiveBrush = new(Color.FromArgb(255, 220, 38, 38)); // #DC2626 (red text)

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isActive)
        {
            return isActive ? ActiveBrush : InactiveBrush;
        }
        return InactiveBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
