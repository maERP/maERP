using Microsoft.UI.Xaml.Data;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts decimal values to currency string format.
/// </summary>
public class CurrencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            decimal decimalValue => decimalValue.ToString("C2"),
            double doubleValue => doubleValue.ToString("C2"),
            float floatValue => floatValue.ToString("C2"),
            int intValue => intValue.ToString("C2"),
            _ => value?.ToString() ?? string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
