namespace maERP.Server.Models;

public enum OrderStatus
{
    open = 1,
    in_progress = 2,
    fully_completed = 3,
    partially_completed = 4,
    cancelled = 5,
    ready_for_delivery = 6,
    partially_delivered = 7,
    fully_delivered = 8,
    clarification = 9
}