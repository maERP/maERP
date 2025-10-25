using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace maERP.UI.Converters;

/// <summary>
/// Converts a boolean value to a Visibility value (inverted).
/// True -> Collapsed, False -> Visible
/// </summary>
public class InverseBoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Collapsed : Visibility.Visible;
        }

        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Collapsed;
        }

        return true;
    }
}
