using System;
using Microsoft.UI.Xaml.Data;

namespace maERP.UI.Shared.Converters;

/// <summary>
/// Inverts a boolean value.
/// True -> False, False -> True
/// </summary>
public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }

        return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }

        return false;
    }
}
