namespace maERP.Domain.Enums;

public enum PaymentStatus
{
    Unknown = 0,
    Invoiced = 1,
    PartiallyPaid = 2,
    CompletelyPaid = 3,
    FirstReminder = 4,
    SecondReminder = 5,
    ThirdReminder = 6,
    Encashment = 7,
    Reserved = 8,
    Delayed = 10,
    ReCrediting = 11,
    ReviewNecessary = 12,
    NoCreditApproved = 13,
    CreditPreliminarilyAccepted = 14
}