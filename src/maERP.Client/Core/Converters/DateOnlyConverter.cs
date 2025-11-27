using Microsoft.UI.Xaml.Data;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts DateTime or DateTimeOffset to date-only string format.
/// </summary>
public class DateOnlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            DateTime dateTime => dateTime.ToString("d"),
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString("d"),
            DateOnly dateOnly => dateOnly.ToString("d"),
            _ => value?.ToString() ?? string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
