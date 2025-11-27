using Microsoft.UI.Xaml.Data;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts a string to Visibility. Returns Collapsed if the string is not null or empty, otherwise Visible.
/// Inverse of StringToVisibilityConverter.
/// </summary>
public class StringToInverseVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string stringValue)
        {
            return string.IsNullOrWhiteSpace(stringValue) ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
