using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace maERP.UI.Shared.Converters;

/// <summary>
/// Converts a boolean value to a Visibility value.
/// True -> Visible, False -> Collapsed
/// </summary>
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }

        return false;
    }
}
