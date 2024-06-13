namespace maERP.Application.Features.Statistic.Queries.StatisticOrderChart;

public class StatisticOrderCustomerChartResponse
{
    public List<OrderCustomerChartDto> chartData { get; set; } = new List<OrderCustomerChartDto>();
}

public class OrderCustomerChartDto
{
    public DateTime Date { get; set; }
    public int OrdersNew { get; set; }
    public int CustomersNew { get; set; }
}