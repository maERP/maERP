namespace maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;

public class StatisticOrderCustomerChartResponse
{
    public List<OrderCustomerChartDto> chartData { get; set; } = new();
}

public class OrderCustomerChartDto
{
    public DateTime Date { get; set; }
    public int OrdersNew { get; set; }
    public int CustomersNew { get; set; }
}