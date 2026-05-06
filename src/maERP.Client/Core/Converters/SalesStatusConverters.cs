using maERP.Domain.Enums;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts SalesStatus enum to localized display text.
/// </summary>
public class SalesStatusToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is SalesStatus status)
        {
            return status switch
            {
                SalesStatus.Unknown => GetLocalizedString("SalesStatus.Unknown"),
                SalesStatus.Pending => GetLocalizedString("SalesStatus.Pending"),
                SalesStatus.Processing => GetLocalizedString("SalesStatus.Processing"),
                SalesStatus.ReadyForDelivery => GetLocalizedString("SalesStatus.ReadyForDelivery"),
                SalesStatus.PartiallyDelivered => GetLocalizedString("SalesStatus.PartiallyDelivered"),
                SalesStatus.Completed => GetLocalizedString("SalesStatus.Completed"),
                SalesStatus.Cancelled => GetLocalizedString("SalesStatus.Cancelled"),
                SalesStatus.Returned => GetLocalizedString("SalesStatus.Returned"),
                SalesStatus.Refunded => GetLocalizedString("SalesStatus.Refunded"),
                SalesStatus.OnHold => GetLocalizedString("SalesStatus.OnHold"),
                SalesStatus.Failed => GetLocalizedString("SalesStatus.Failed"),
                _ => status.ToString()
            };
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

    private static string GetLocalizedString(string resourceKey)
    {
        return resourceKey switch
        {
            "SalesStatus.Unknown" => LocalizationHelper.GetLocalizedString(resourceKey, "Unknown"),
            "SalesStatus.Pending" => LocalizationHelper.GetLocalizedString(resourceKey, "Pending"),
            "SalesStatus.Processing" => LocalizationHelper.GetLocalizedString(resourceKey, "Processing"),
            "SalesStatus.ReadyForDelivery" => LocalizationHelper.GetLocalizedString(resourceKey, "Ready for Delivery"),
            "SalesStatus.PartiallyDelivered" => LocalizationHelper.GetLocalizedString(resourceKey, "Partially Delivered"),
            "SalesStatus.Completed" => LocalizationHelper.GetLocalizedString(resourceKey, "Completed"),
            "SalesStatus.Cancelled" => LocalizationHelper.GetLocalizedString(resourceKey, "Cancelled"),
            "SalesStatus.Returned" => LocalizationHelper.GetLocalizedString(resourceKey, "Returned"),
            "SalesStatus.Refunded" => LocalizationHelper.GetLocalizedString(resourceKey, "Refunded"),
            "SalesStatus.OnHold" => LocalizationHelper.GetLocalizedString(resourceKey, "On Hold"),
            "SalesStatus.Failed" => LocalizationHelper.GetLocalizedString(resourceKey, "Failed"),
            _ => resourceKey
        };
    }
}

/// <summary>
/// Converts SalesStatus enum to background brush color.
/// </summary>
public class SalesStatusToBackgroundConverter : IValueConverter
{
    // Green shades for positive states
    private static readonly SolidColorBrush CompletedBrush = new(Color.FromArgb(255, 220, 252, 231)); // #DCFCE7 - green
    // Blue shades for in-progress states
    private static readonly SolidColorBrush ProcessingBrush = new(Color.FromArgb(255, 219, 234, 254)); // #DBEAFE - blue
    private static readonly SolidColorBrush ReadyForDeliveryBrush = new(Color.FromArgb(255, 167, 243, 208)); // #A7F3D0 - teal
    // Yellow/Orange shades for warning states
    private static readonly SolidColorBrush PendingBrush = new(Color.FromArgb(255, 254, 243, 199)); // #FEF3C7 - yellow
    private static readonly SolidColorBrush PartiallyDeliveredBrush = new(Color.FromArgb(255, 254, 243, 199)); // #FEF3C7 - yellow
    private static readonly SolidColorBrush OnHoldBrush = new(Color.FromArgb(255, 255, 237, 213)); // #FFEDD5 - orange
    // Red shades for problem states
    private static readonly SolidColorBrush CancelledBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray
    private static readonly SolidColorBrush FailedBrush = new(Color.FromArgb(255, 254, 226, 226)); // #FEE2E2 - red
    // Purple for special states
    private static readonly SolidColorBrush ReturnedBrush = new(Color.FromArgb(255, 255, 237, 213)); // #FFEDD5 - orange
    private static readonly SolidColorBrush RefundedBrush = new(Color.FromArgb(255, 243, 232, 255)); // #F3E8FF - purple
    private static readonly SolidColorBrush UnknownBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is SalesStatus status)
        {
            return status switch
            {
                SalesStatus.Completed => CompletedBrush,
                SalesStatus.Processing => ProcessingBrush,
                SalesStatus.ReadyForDelivery => ReadyForDeliveryBrush,
                SalesStatus.Pending => PendingBrush,
                SalesStatus.PartiallyDelivered => PartiallyDeliveredBrush,
                SalesStatus.OnHold => OnHoldBrush,
                SalesStatus.Cancelled => CancelledBrush,
                SalesStatus.Failed => FailedBrush,
                SalesStatus.Returned => ReturnedBrush,
                SalesStatus.Refunded => RefundedBrush,
                _ => UnknownBrush
            };
        }
        return UnknownBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts SalesStatus enum to foreground brush color.
/// </summary>
public class SalesStatusToForegroundConverter : IValueConverter
{
    // Green shades for positive states
    private static readonly SolidColorBrush CompletedBrush = new(Color.FromArgb(255, 22, 163, 74)); // #16A34A - green
    // Blue shades for in-progress states
    private static readonly SolidColorBrush ProcessingBrush = new(Color.FromArgb(255, 37, 99, 235)); // #2563EB - blue
    private static readonly SolidColorBrush ReadyForDeliveryBrush = new(Color.FromArgb(255, 5, 150, 105)); // #059669 - teal
    // Yellow/Orange shades for warning states
    private static readonly SolidColorBrush PendingBrush = new(Color.FromArgb(255, 217, 119, 6)); // #D97706 - yellow
    private static readonly SolidColorBrush PartiallyDeliveredBrush = new(Color.FromArgb(255, 217, 119, 6)); // #D97706 - yellow
    private static readonly SolidColorBrush OnHoldBrush = new(Color.FromArgb(255, 234, 88, 12)); // #EA580C - orange
    // Red shades for problem states
    private static readonly SolidColorBrush CancelledBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray
    private static readonly SolidColorBrush FailedBrush = new(Color.FromArgb(255, 220, 38, 38)); // #DC2626 - red
    // Purple for special states
    private static readonly SolidColorBrush ReturnedBrush = new(Color.FromArgb(255, 234, 88, 12)); // #EA580C - orange
    private static readonly SolidColorBrush RefundedBrush = new(Color.FromArgb(255, 147, 51, 234)); // #9333EA - purple
    private static readonly SolidColorBrush UnknownBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is SalesStatus status)
        {
            return status switch
            {
                SalesStatus.Completed => CompletedBrush,
                SalesStatus.Processing => ProcessingBrush,
                SalesStatus.ReadyForDelivery => ReadyForDeliveryBrush,
                SalesStatus.Pending => PendingBrush,
                SalesStatus.PartiallyDelivered => PartiallyDeliveredBrush,
                SalesStatus.OnHold => OnHoldBrush,
                SalesStatus.Cancelled => CancelledBrush,
                SalesStatus.Failed => FailedBrush,
                SalesStatus.Returned => ReturnedBrush,
                SalesStatus.Refunded => RefundedBrush,
                _ => UnknownBrush
            };
        }
        return UnknownBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
