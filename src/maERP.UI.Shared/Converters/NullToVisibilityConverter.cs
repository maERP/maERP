using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace maERP.UI.Converters;

/// <summary>
/// Converts a null value to Visibility.
/// Null -> Collapsed, Non-null -> Visible
/// Use ConverterParameter="Inverse" to invert the logic
/// </summary>
public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        bool isInverse = parameter?.ToString()?.Equals("Inverse", StringComparison.OrdinalIgnoreCase) ?? false;
        bool isNull = value == null || (value is string str && string.IsNullOrWhiteSpace(str));

        if (isInverse)
        {
            return isNull ? Visibility.Visible : Visibility.Collapsed;
        }

        return isNull ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
