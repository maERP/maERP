namespace maERP.Application.Features.Statistic.Queries.StatisticSalesCustomerChart;

public class StatisticSalesCustomerChartResponse
{
    public List<SalesCustomerChartDto> chartData { get; set; } = new();
}

public class SalesCustomerChartDto
{
    public DateTime Date { get; set; }
    public int SalessNew { get; set; }
    public int CustomersNew { get; set; }
}

// TODO: remove this classes and use the ones in maERP.Domain.Dtos