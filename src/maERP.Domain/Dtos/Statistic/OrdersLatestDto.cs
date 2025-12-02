using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for latest orders statistics (dashboard recent orders card)
/// </summary>
public class OrdersLatestDto
{
    /// <summary>
    /// List of recent orders
    /// </summary>
    public List<OrdersLatestItemDto> Orders { get; set; } = new();
}

/// <summary>
/// Single order item in the latest orders list
/// </summary>
public class OrdersLatestItemDto
{
    /// <summary>
    /// Order ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Order number for display
    /// </summary>
    public string OrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// Customer name
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Order total amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Date when order was placed
    /// </summary>
    public DateTime OrderDate { get; set; }
}
