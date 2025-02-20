namespace maERP.Domain.Dtos.Statistic;

public class DailyStatistic
{
    public DateTime Date { get; set; }
    public int OrderCount { get; set; }
    public int NewCustomerCount { get; set; }
}

public class StatisticOrderDto
{
    public int Order30Days { get; set; }
    public int OrderTotal { get; set; }
    public int CustomerTotal { get; set; }
    public List<DailyStatistic> DailyStatistics { get; set; } = new();
}