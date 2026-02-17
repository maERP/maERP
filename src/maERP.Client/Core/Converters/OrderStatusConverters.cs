using maERP.Domain.Enums;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts OrderStatus enum to localized display text.
/// </summary>
public class OrderStatusToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Unknown => GetLocalizedString("OrderStatus.Unknown"),
                OrderStatus.Pending => GetLocalizedString("OrderStatus.Pending"),
                OrderStatus.Processing => GetLocalizedString("OrderStatus.Processing"),
                OrderStatus.ReadyForDelivery => GetLocalizedString("OrderStatus.ReadyForDelivery"),
                OrderStatus.PartiallyDelivered => GetLocalizedString("OrderStatus.PartiallyDelivered"),
                OrderStatus.Completed => GetLocalizedString("OrderStatus.Completed"),
                OrderStatus.Cancelled => GetLocalizedString("OrderStatus.Cancelled"),
                OrderStatus.Returned => GetLocalizedString("OrderStatus.Returned"),
                OrderStatus.Refunded => GetLocalizedString("OrderStatus.Refunded"),
                OrderStatus.OnHold => GetLocalizedString("OrderStatus.OnHold"),
                OrderStatus.Failed => GetLocalizedString("OrderStatus.Failed"),
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
            "OrderStatus.Unknown" => LocalizationHelper.GetLocalizedString(resourceKey, "Unknown"),
            "OrderStatus.Pending" => LocalizationHelper.GetLocalizedString(resourceKey, "Pending"),
            "OrderStatus.Processing" => LocalizationHelper.GetLocalizedString(resourceKey, "Processing"),
            "OrderStatus.ReadyForDelivery" => LocalizationHelper.GetLocalizedString(resourceKey, "Ready for Delivery"),
            "OrderStatus.PartiallyDelivered" => LocalizationHelper.GetLocalizedString(resourceKey, "Partially Delivered"),
            "OrderStatus.Completed" => LocalizationHelper.GetLocalizedString(resourceKey, "Completed"),
            "OrderStatus.Cancelled" => LocalizationHelper.GetLocalizedString(resourceKey, "Cancelled"),
            "OrderStatus.Returned" => LocalizationHelper.GetLocalizedString(resourceKey, "Returned"),
            "OrderStatus.Refunded" => LocalizationHelper.GetLocalizedString(resourceKey, "Refunded"),
            "OrderStatus.OnHold" => LocalizationHelper.GetLocalizedString(resourceKey, "On Hold"),
            "OrderStatus.Failed" => LocalizationHelper.GetLocalizedString(resourceKey, "Failed"),
            _ => resourceKey
        };
    }
}

/// <summary>
/// Converts OrderStatus enum to background brush color.
/// </summary>
public class OrderStatusToBackgroundConverter : IValueConverter
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
        if (value is OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Completed => CompletedBrush,
                OrderStatus.Processing => ProcessingBrush,
                OrderStatus.ReadyForDelivery => ReadyForDeliveryBrush,
                OrderStatus.Pending => PendingBrush,
                OrderStatus.PartiallyDelivered => PartiallyDeliveredBrush,
                OrderStatus.OnHold => OnHoldBrush,
                OrderStatus.Cancelled => CancelledBrush,
                OrderStatus.Failed => FailedBrush,
                OrderStatus.Returned => ReturnedBrush,
                OrderStatus.Refunded => RefundedBrush,
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
/// Converts OrderStatus enum to foreground brush color.
/// </summary>
public class OrderStatusToForegroundConverter : IValueConverter
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
        if (value is OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Completed => CompletedBrush,
                OrderStatus.Processing => ProcessingBrush,
                OrderStatus.ReadyForDelivery => ReadyForDeliveryBrush,
                OrderStatus.Pending => PendingBrush,
                OrderStatus.PartiallyDelivered => PartiallyDeliveredBrush,
                OrderStatus.OnHold => OnHoldBrush,
                OrderStatus.Cancelled => CancelledBrush,
                OrderStatus.Failed => FailedBrush,
                OrderStatus.Returned => ReturnedBrush,
                OrderStatus.Refunded => RefundedBrush,
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
