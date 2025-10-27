using System;
using Microsoft.UI.Xaml.Data;

namespace maERP.UI.Shared.Converters;

/// <summary>
/// Converts a null value to a boolean.
/// Null -> False, Non-null -> True
/// Use ConverterParameter="Inverse" to invert the logic
/// </summary>
public class NullToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        bool isInverse = parameter?.ToString()?.Equals("Inverse", StringComparison.OrdinalIgnoreCase) ?? false;
        bool isNull = value == null || (value is string str && string.IsNullOrWhiteSpace(str));

        if (isInverse)
        {
            return isNull;
        }

        return !isNull;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
