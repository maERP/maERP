namespace maERP.Domain.Dtos.Statistic;

public class MostSellingProductItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductSku { get; set; } = string.Empty;
    public double TotalQuantity { get; set; }
}

public class StatisticMostSellingProductsDto
{
    public List<MostSellingProductItem> TopProductsToday { get; set; } = new();
    public List<MostSellingProductItem> TopProductsLastSevenDays { get; set; } = new();
    public List<MostSellingProductItem> TopProductsThisMonth { get; set; } = new();
    public List<MostSellingProductItem> TopProductsThisYear { get; set; } = new();
    public List<MostSellingProductItem> TopProductsAllTime { get; set; } = new();
}