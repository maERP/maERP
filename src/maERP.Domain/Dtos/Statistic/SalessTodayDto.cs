namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for saless statistics (second KPI card)
/// </summary>
public class SalessTodayDto
{
    /// <summary>
    /// Number of saless today
    /// </summary>
    public int SalessToday { get; set; }

    /// <summary>
    /// Number of pending saless
    /// </summary>
    public int SalessPending { get; set; }

    /// <summary>
    /// Number of saless this week
    /// </summary>
    public int SalessThisWeek { get; set; }

    /// <summary>
    /// Saless percentage change compared to last week
    /// </summary>
    public decimal SalessChangePercent { get; set; }
}
