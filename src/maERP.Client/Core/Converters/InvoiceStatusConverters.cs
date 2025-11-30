using maERP.Domain.Enums;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace maERP.Client.Presentation;

/// <summary>
/// Converts InvoiceStatus enum to localized display text.
/// </summary>
public class InvoiceStatusToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is InvoiceStatus status)
        {
            return status switch
            {
                InvoiceStatus.Unknown => GetLocalizedString("InvoiceStatus.Unknown"),
                InvoiceStatus.Created => GetLocalizedString("InvoiceStatus.Created"),
                InvoiceStatus.Sent => GetLocalizedString("InvoiceStatus.Sent"),
                InvoiceStatus.Overdue => GetLocalizedString("InvoiceStatus.Overdue"),
                InvoiceStatus.PartiallyPaid => GetLocalizedString("InvoiceStatus.PartiallyPaid"),
                InvoiceStatus.Paid => GetLocalizedString("InvoiceStatus.Paid"),
                InvoiceStatus.Cancelled => GetLocalizedString("InvoiceStatus.Cancelled"),
                InvoiceStatus.Disputed => GetLocalizedString("InvoiceStatus.Disputed"),
                InvoiceStatus.Refunded => GetLocalizedString("InvoiceStatus.Refunded"),
                InvoiceStatus.WrittenOff => GetLocalizedString("InvoiceStatus.WrittenOff"),
                InvoiceStatus.Archived => GetLocalizedString("InvoiceStatus.Archived"),
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
        try
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            var result = resourceLoader.GetString(resourceKey);
            return !string.IsNullOrEmpty(result) ? result : GetFallbackString(resourceKey);
        }
        catch
        {
            return GetFallbackString(resourceKey);
        }
    }

    private static string GetFallbackString(string resourceKey)
    {
        return resourceKey switch
        {
            "InvoiceStatus.Unknown" => "Unknown",
            "InvoiceStatus.Created" => "Created",
            "InvoiceStatus.Sent" => "Sent",
            "InvoiceStatus.Overdue" => "Overdue",
            "InvoiceStatus.PartiallyPaid" => "Partially Paid",
            "InvoiceStatus.Paid" => "Paid",
            "InvoiceStatus.Cancelled" => "Cancelled",
            "InvoiceStatus.Disputed" => "Disputed",
            "InvoiceStatus.Refunded" => "Refunded",
            "InvoiceStatus.WrittenOff" => "Written Off",
            "InvoiceStatus.Archived" => "Archived",
            _ => resourceKey
        };
    }
}

/// <summary>
/// Converts InvoiceStatus enum to background brush color.
/// </summary>
public class InvoiceStatusToBackgroundConverter : IValueConverter
{
    // Green shades for positive states
    private static readonly SolidColorBrush PaidBrush = new(Color.FromArgb(255, 220, 252, 231)); // #DCFCE7 - green
    // Blue shades for in-progress states
    private static readonly SolidColorBrush SentBrush = new(Color.FromArgb(255, 219, 234, 254)); // #DBEAFE - blue
    private static readonly SolidColorBrush CreatedBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray
    // Yellow/Orange shades for warning states
    private static readonly SolidColorBrush PartiallyPaidBrush = new(Color.FromArgb(255, 254, 243, 199)); // #FEF3C7 - yellow
    private static readonly SolidColorBrush OverdueBrush = new(Color.FromArgb(255, 255, 237, 213)); // #FFEDD5 - orange
    // Red shades for problem states
    private static readonly SolidColorBrush DisputedBrush = new(Color.FromArgb(255, 254, 226, 226)); // #FEE2E2 - red
    private static readonly SolidColorBrush CancelledBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray
    // Purple for special states
    private static readonly SolidColorBrush RefundedBrush = new(Color.FromArgb(255, 243, 232, 255)); // #F3E8FF - purple
    private static readonly SolidColorBrush WrittenOffBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray
    private static readonly SolidColorBrush ArchivedBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is InvoiceStatus status)
        {
            return status switch
            {
                InvoiceStatus.Paid => PaidBrush,
                InvoiceStatus.Sent => SentBrush,
                InvoiceStatus.Created => CreatedBrush,
                InvoiceStatus.PartiallyPaid => PartiallyPaidBrush,
                InvoiceStatus.Overdue => OverdueBrush,
                InvoiceStatus.Disputed => DisputedBrush,
                InvoiceStatus.Cancelled => CancelledBrush,
                InvoiceStatus.Refunded => RefundedBrush,
                InvoiceStatus.WrittenOff => WrittenOffBrush,
                InvoiceStatus.Archived => ArchivedBrush,
                _ => CreatedBrush
            };
        }
        return CreatedBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts InvoiceStatus enum to foreground brush color.
/// </summary>
public class InvoiceStatusToForegroundConverter : IValueConverter
{
    // Green shades for positive states
    private static readonly SolidColorBrush PaidBrush = new(Color.FromArgb(255, 22, 163, 74)); // #16A34A - green
    // Blue shades for in-progress states
    private static readonly SolidColorBrush SentBrush = new(Color.FromArgb(255, 37, 99, 235)); // #2563EB - blue
    private static readonly SolidColorBrush CreatedBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray
    // Yellow/Orange shades for warning states
    private static readonly SolidColorBrush PartiallyPaidBrush = new(Color.FromArgb(255, 217, 119, 6)); // #D97706 - yellow
    private static readonly SolidColorBrush OverdueBrush = new(Color.FromArgb(255, 234, 88, 12)); // #EA580C - orange
    // Red shades for problem states
    private static readonly SolidColorBrush DisputedBrush = new(Color.FromArgb(255, 220, 38, 38)); // #DC2626 - red
    private static readonly SolidColorBrush CancelledBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray
    // Purple for special states
    private static readonly SolidColorBrush RefundedBrush = new(Color.FromArgb(255, 147, 51, 234)); // #9333EA - purple
    private static readonly SolidColorBrush WrittenOffBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray
    private static readonly SolidColorBrush ArchivedBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is InvoiceStatus status)
        {
            return status switch
            {
                InvoiceStatus.Paid => PaidBrush,
                InvoiceStatus.Sent => SentBrush,
                InvoiceStatus.Created => CreatedBrush,
                InvoiceStatus.PartiallyPaid => PartiallyPaidBrush,
                InvoiceStatus.Overdue => OverdueBrush,
                InvoiceStatus.Disputed => DisputedBrush,
                InvoiceStatus.Cancelled => CancelledBrush,
                InvoiceStatus.Refunded => RefundedBrush,
                InvoiceStatus.WrittenOff => WrittenOffBrush,
                InvoiceStatus.Archived => ArchivedBrush,
                _ => CreatedBrush
            };
        }
        return CreatedBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts PaymentStatus enum to localized display text.
/// </summary>
public class PaymentStatusToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is PaymentStatus status)
        {
            return status switch
            {
                PaymentStatus.Unknown => GetLocalizedString("PaymentStatus.Unknown"),
                PaymentStatus.Invoiced => GetLocalizedString("PaymentStatus.Invoiced"),
                PaymentStatus.PartiallyPaid => GetLocalizedString("PaymentStatus.PartiallyPaid"),
                PaymentStatus.CompletelyPaid => GetLocalizedString("PaymentStatus.CompletelyPaid"),
                PaymentStatus.FirstReminder => GetLocalizedString("PaymentStatus.FirstReminder"),
                PaymentStatus.SecondReminder => GetLocalizedString("PaymentStatus.SecondReminder"),
                PaymentStatus.ThirdReminder => GetLocalizedString("PaymentStatus.ThirdReminder"),
                PaymentStatus.Encashment => GetLocalizedString("PaymentStatus.Encashment"),
                PaymentStatus.Reserved => GetLocalizedString("PaymentStatus.Reserved"),
                PaymentStatus.Delayed => GetLocalizedString("PaymentStatus.Delayed"),
                PaymentStatus.ReCrediting => GetLocalizedString("PaymentStatus.ReCrediting"),
                PaymentStatus.ReviewNecessary => GetLocalizedString("PaymentStatus.ReviewNecessary"),
                PaymentStatus.NoCreditApproved => GetLocalizedString("PaymentStatus.NoCreditApproved"),
                PaymentStatus.CreditPreliminarilyAccepted => GetLocalizedString("PaymentStatus.CreditPreliminarilyAccepted"),
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
        try
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            var result = resourceLoader.GetString(resourceKey);
            return !string.IsNullOrEmpty(result) ? result : GetFallbackString(resourceKey);
        }
        catch
        {
            return GetFallbackString(resourceKey);
        }
    }

    private static string GetFallbackString(string resourceKey)
    {
        return resourceKey switch
        {
            "PaymentStatus.Unknown" => "Unknown",
            "PaymentStatus.Invoiced" => "Invoiced",
            "PaymentStatus.PartiallyPaid" => "Partially Paid",
            "PaymentStatus.CompletelyPaid" => "Completely Paid",
            "PaymentStatus.FirstReminder" => "1st Reminder",
            "PaymentStatus.SecondReminder" => "2nd Reminder",
            "PaymentStatus.ThirdReminder" => "3rd Reminder",
            "PaymentStatus.Encashment" => "Collection",
            "PaymentStatus.Reserved" => "Reserved",
            "PaymentStatus.Delayed" => "Delayed",
            "PaymentStatus.ReCrediting" => "Re-crediting",
            "PaymentStatus.ReviewNecessary" => "Review Required",
            "PaymentStatus.NoCreditApproved" => "Credit Rejected",
            "PaymentStatus.CreditPreliminarilyAccepted" => "Credit Pending",
            _ => resourceKey
        };
    }
}

/// <summary>
/// Converts PaymentStatus enum to background brush color.
/// </summary>
public class PaymentStatusToBackgroundConverter : IValueConverter
{
    // Green shades for positive states
    private static readonly SolidColorBrush CompletelyPaidBrush = new(Color.FromArgb(255, 220, 252, 231)); // #DCFCE7 - green
    // Blue shades for neutral states
    private static readonly SolidColorBrush InvoicedBrush = new(Color.FromArgb(255, 219, 234, 254)); // #DBEAFE - blue
    private static readonly SolidColorBrush ReservedBrush = new(Color.FromArgb(255, 219, 234, 254)); // #DBEAFE - blue
    // Yellow/Orange shades for warning states
    private static readonly SolidColorBrush PartiallyPaidBrush = new(Color.FromArgb(255, 254, 243, 199)); // #FEF3C7 - yellow
    private static readonly SolidColorBrush FirstReminderBrush = new(Color.FromArgb(255, 254, 243, 199)); // #FEF3C7 - yellow
    private static readonly SolidColorBrush SecondReminderBrush = new(Color.FromArgb(255, 255, 237, 213)); // #FFEDD5 - orange
    private static readonly SolidColorBrush ThirdReminderBrush = new(Color.FromArgb(255, 255, 237, 213)); // #FFEDD5 - orange
    private static readonly SolidColorBrush DelayedBrush = new(Color.FromArgb(255, 255, 237, 213)); // #FFEDD5 - orange
    // Red shades for problem states
    private static readonly SolidColorBrush EncashmentBrush = new(Color.FromArgb(255, 254, 226, 226)); // #FEE2E2 - red
    private static readonly SolidColorBrush NoCreditApprovedBrush = new(Color.FromArgb(255, 254, 226, 226)); // #FEE2E2 - red
    private static readonly SolidColorBrush ReviewNecessaryBrush = new(Color.FromArgb(255, 254, 226, 226)); // #FEE2E2 - red
    // Purple for special states
    private static readonly SolidColorBrush ReCreditingBrush = new(Color.FromArgb(255, 243, 232, 255)); // #F3E8FF - purple
    private static readonly SolidColorBrush CreditPendingBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray
    private static readonly SolidColorBrush UnknownBrush = new(Color.FromArgb(255, 243, 244, 246)); // #F3F4F6 - gray

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is PaymentStatus status)
        {
            return status switch
            {
                PaymentStatus.CompletelyPaid => CompletelyPaidBrush,
                PaymentStatus.Invoiced => InvoicedBrush,
                PaymentStatus.Reserved => ReservedBrush,
                PaymentStatus.PartiallyPaid => PartiallyPaidBrush,
                PaymentStatus.FirstReminder => FirstReminderBrush,
                PaymentStatus.SecondReminder => SecondReminderBrush,
                PaymentStatus.ThirdReminder => ThirdReminderBrush,
                PaymentStatus.Delayed => DelayedBrush,
                PaymentStatus.Encashment => EncashmentBrush,
                PaymentStatus.NoCreditApproved => NoCreditApprovedBrush,
                PaymentStatus.ReviewNecessary => ReviewNecessaryBrush,
                PaymentStatus.ReCrediting => ReCreditingBrush,
                PaymentStatus.CreditPreliminarilyAccepted => CreditPendingBrush,
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
/// Converts PaymentStatus enum to foreground brush color.
/// </summary>
public class PaymentStatusToForegroundConverter : IValueConverter
{
    // Green shades for positive states
    private static readonly SolidColorBrush CompletelyPaidBrush = new(Color.FromArgb(255, 22, 163, 74)); // #16A34A - green
    // Blue shades for neutral states
    private static readonly SolidColorBrush InvoicedBrush = new(Color.FromArgb(255, 37, 99, 235)); // #2563EB - blue
    private static readonly SolidColorBrush ReservedBrush = new(Color.FromArgb(255, 37, 99, 235)); // #2563EB - blue
    // Yellow/Orange shades for warning states
    private static readonly SolidColorBrush PartiallyPaidBrush = new(Color.FromArgb(255, 217, 119, 6)); // #D97706 - yellow
    private static readonly SolidColorBrush FirstReminderBrush = new(Color.FromArgb(255, 217, 119, 6)); // #D97706 - yellow
    private static readonly SolidColorBrush SecondReminderBrush = new(Color.FromArgb(255, 234, 88, 12)); // #EA580C - orange
    private static readonly SolidColorBrush ThirdReminderBrush = new(Color.FromArgb(255, 234, 88, 12)); // #EA580C - orange
    private static readonly SolidColorBrush DelayedBrush = new(Color.FromArgb(255, 234, 88, 12)); // #EA580C - orange
    // Red shades for problem states
    private static readonly SolidColorBrush EncashmentBrush = new(Color.FromArgb(255, 220, 38, 38)); // #DC2626 - red
    private static readonly SolidColorBrush NoCreditApprovedBrush = new(Color.FromArgb(255, 220, 38, 38)); // #DC2626 - red
    private static readonly SolidColorBrush ReviewNecessaryBrush = new(Color.FromArgb(255, 220, 38, 38)); // #DC2626 - red
    // Purple for special states
    private static readonly SolidColorBrush ReCreditingBrush = new(Color.FromArgb(255, 147, 51, 234)); // #9333EA - purple
    private static readonly SolidColorBrush CreditPendingBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray
    private static readonly SolidColorBrush UnknownBrush = new(Color.FromArgb(255, 107, 114, 128)); // #6B7280 - gray

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is PaymentStatus status)
        {
            return status switch
            {
                PaymentStatus.CompletelyPaid => CompletelyPaidBrush,
                PaymentStatus.Invoiced => InvoicedBrush,
                PaymentStatus.Reserved => ReservedBrush,
                PaymentStatus.PartiallyPaid => PartiallyPaidBrush,
                PaymentStatus.FirstReminder => FirstReminderBrush,
                PaymentStatus.SecondReminder => SecondReminderBrush,
                PaymentStatus.ThirdReminder => ThirdReminderBrush,
                PaymentStatus.Delayed => DelayedBrush,
                PaymentStatus.Encashment => EncashmentBrush,
                PaymentStatus.NoCreditApproved => NoCreditApprovedBrush,
                PaymentStatus.ReviewNecessary => ReviewNecessaryBrush,
                PaymentStatus.ReCrediting => ReCreditingBrush,
                PaymentStatus.CreditPreliminarilyAccepted => CreditPendingBrush,
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
