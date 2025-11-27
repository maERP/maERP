using Microsoft.UI.Xaml.Data;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts a string to Visibility. Returns Visible if the string is not null or empty, otherwise Collapsed.
/// </summary>
public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string stringValue)
        {
            return string.IsNullOrWhiteSpace(stringValue) ? Visibility.Collapsed : Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
