namespace maERP.Domain.Enums;

public enum ChannelOutboxStatus
{
    Pending = 0,
    InFlight = 1,
    Done = 2,
    DeadLetter = 3,
}
