namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for customer statistics (third KPI card)
/// </summary>
public class CustomersTodayDto
{
    /// <summary>
    /// Total number of customers
    /// </summary>
    public int CustomersTotal { get; set; }

    /// <summary>
    /// New customers this month
    /// </summary>
    public int CustomersNewThisMonth { get; set; }

    /// <summary>
    /// Customers percentage change compared to last month
    /// </summary>
    public decimal CustomersChangePercent { get; set; }
}
