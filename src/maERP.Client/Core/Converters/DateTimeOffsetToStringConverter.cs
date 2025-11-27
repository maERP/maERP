using Microsoft.UI.Xaml.Data;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts DateTimeOffset to a localized date string format.
/// </summary>
public class DateTimeOffsetToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.LocalDateTime.ToString("d");
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
