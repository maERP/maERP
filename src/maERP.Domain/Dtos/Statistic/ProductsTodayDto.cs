namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for products/inventory statistics (fourth KPI card)
/// </summary>
public class ProductsTodayDto
{
    /// <summary>
    /// Number of products in stock
    /// </summary>
    public int ProductsInStock { get; set; }

    /// <summary>
    /// Total number of products
    /// </summary>
    public int ProductsTotal { get; set; }

    /// <summary>
    /// Number of products with low stock
    /// </summary>
    public int ProductsLowStock { get; set; }

    /// <summary>
    /// Products percentage change compared to last week
    /// </summary>
    public decimal ProductsChangePercent { get; set; }
}
