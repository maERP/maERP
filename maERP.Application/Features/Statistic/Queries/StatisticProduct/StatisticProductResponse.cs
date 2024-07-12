namespace maERP.Application.Features.Statistic.Queries.StatisticProduct;

public class StatisticProductResponse
{
    public int ProductInStock { get; set; }
    public int ProductTotal { get; set; }
    public double ProductInWarehouse { get; set; }
    public double ProductValue { get; set; }
}