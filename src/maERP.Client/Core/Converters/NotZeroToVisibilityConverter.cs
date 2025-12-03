using Microsoft.UI.Xaml.Data;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts a numeric value to Visibility. Returns Visible if the value is NOT zero, otherwise Collapsed.
/// Useful for showing content when a collection has items.
/// </summary>
public class NotZeroToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            int intValue => intValue != 0 ? Visibility.Visible : Visibility.Collapsed,
            long longValue => longValue != 0 ? Visibility.Visible : Visibility.Collapsed,
            double doubleValue => doubleValue != 0 ? Visibility.Visible : Visibility.Collapsed,
            _ => Visibility.Collapsed
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
