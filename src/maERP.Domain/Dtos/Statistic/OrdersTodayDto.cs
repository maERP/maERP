namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for orders statistics (second KPI card)
/// </summary>
public class OrdersTodayDto
{
    /// <summary>
    /// Number of orders today
    /// </summary>
    public int OrdersToday { get; set; }

    /// <summary>
    /// Number of pending orders
    /// </summary>
    public int OrdersPending { get; set; }

    /// <summary>
    /// Number of orders this week
    /// </summary>
    public int OrdersThisWeek { get; set; }

    /// <summary>
    /// Orders percentage change compared to last week
    /// </summary>
    public decimal OrdersChangePercent { get; set; }
}
