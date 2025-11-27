using maERP.Domain.Enums;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts CustomerStatus enum to localized display text.
/// </summary>
public class CustomerStatusToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is CustomerStatus status)
        {
            return status switch
            {
                CustomerStatus.Active => GetLocalizedString("CustomerStatus.Active"),
                CustomerStatus.Inactive => GetLocalizedString("CustomerStatus.Inactive"),
                CustomerStatus.NoDoi => GetLocalizedString("CustomerStatus.NoDoi"),
                _ => status.ToString()
            };
        }
        return string.Empty;
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
            "CustomerStatus.Active" => "Active",
            "CustomerStatus.Inactive" => "Inactive",
            "CustomerStatus.NoDoi" => "No DOI",
            _ => resourceKey
        };
    }
}

/// <summary>
/// Converts CustomerStatus enum to background brush color.
/// </summary>
public class CustomerStatusToBackgroundConverter : IValueConverter
{
    private static readonly SolidColorBrush ActiveBrush = new(Color.FromArgb(255, 220, 252, 231)); // #DCFCE7
    private static readonly SolidColorBrush InactiveBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6
    private static readonly SolidColorBrush NoDoiBrush = new(Color.FromArgb(255, 254, 243, 199)); // #FEF3C7

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is CustomerStatus status)
        {
            return status switch
            {
                CustomerStatus.Active => ActiveBrush,
                CustomerStatus.Inactive => InactiveBrush,
                CustomerStatus.NoDoi => NoDoiBrush,
                _ => InactiveBrush
            };
        }
        return InactiveBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts CustomerStatus enum to foreground brush color.
/// </summary>
public class CustomerStatusToForegroundConverter : IValueConverter
{
    private static readonly SolidColorBrush ActiveBrush = new(Color.FromArgb(255, 22, 163, 74)); // #16A34A
    private static readonly SolidColorBrush InactiveBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280
    private static readonly SolidColorBrush NoDoiBrush = new(Color.FromArgb(255, 217, 119, 6)); // #D97706

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is CustomerStatus status)
        {
            return status switch
            {
                CustomerStatus.Active => ActiveBrush,
                CustomerStatus.Inactive => InactiveBrush,
                CustomerStatus.NoDoi => NoDoiBrush,
                _ => InactiveBrush
            };
        }
        return InactiveBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
