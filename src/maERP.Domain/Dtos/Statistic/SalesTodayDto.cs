namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for revenue/sales statistics (first KPI card)
/// </summary>
public class SalesTodayDto
{
    /// <summary>
    /// Total revenue for today
    /// </summary>
    public decimal RevenueToday { get; set; }

    /// <summary>
    /// Total revenue for the current week
    /// </summary>
    public decimal RevenueThisWeek { get; set; }

    /// <summary>
    /// Total revenue for the current month
    /// </summary>
    public decimal RevenueThisMonth { get; set; }

    /// <summary>
    /// Percentage change compared to last week's same day
    /// </summary>
    public decimal RevenueChangePercent { get; set; }
}
