namespace maERP.Domain.Dtos.Statistic;

public class DailyStatistic
{
    public DateTime Date { get; set; }
    public int SalesCount { get; set; }
    public int NewCustomerCount { get; set; }
}

public class StatisticSalesOverviewDto
{
    public int Sales30Days { get; set; }
    public int SalesTotal { get; set; }
    public int CustomerTotal { get; set; }
    public List<DailyStatistic> DailyStatistics { get; set; } = new();
}