using System;
using Microsoft.UI.Xaml.Data;

namespace maERP.UI.Converters;

/// <summary>
/// Formats a value using a format string provided in ConverterParameter.
/// Example: {Binding Value, Converter={StaticResource StringFormatConverter}, ConverterParameter='Total: {0}'}
/// </summary>
public class StringFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (parameter is string format && !string.IsNullOrEmpty(format))
        {
            try
            {
                return string.Format(format, value);
            }
            catch
            {
                return value?.ToString() ?? string.Empty;
            }
        }

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
