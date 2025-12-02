namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for best-selling products statistics (dashboard top products card)
/// </summary>
public class ProductsBestSellingDto
{
    /// <summary>
    /// List of best-selling products
    /// </summary>
    public List<ProductsBestSellingItemDto> Products { get; set; } = new();
}

/// <summary>
/// Single product item in the best-selling products list
/// </summary>
public class ProductsBestSellingItemDto
{
    /// <summary>
    /// Rank position (1 = best selling)
    /// </summary>
    public int Rank { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Product SKU
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Total quantity sold
    /// </summary>
    public int QuantitySold { get; set; }

    /// <summary>
    /// Total revenue from this product
    /// </summary>
    public decimal Revenue { get; set; }
}
