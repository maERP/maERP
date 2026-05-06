using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Statistic;

/// <summary>
/// DTO for latest saless statistics (dashboard recent saless card)
/// </summary>
public class SalessLatestDto
{
    /// <summary>
    /// List of recent saless
    /// </summary>
    public List<SalessLatestItemDto> Saless { get; set; } = new();
}

/// <summary>
/// Single sales item in the latest saless list
/// </summary>
public class SalessLatestItemDto
{
    /// <summary>
    /// Sales ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Sales number for display
    /// </summary>
    public string SalesNumber { get; set; } = string.Empty;

    /// <summary>
    /// Customer name
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Sales total amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Sales status
    /// </summary>
    public SalesStatus Status { get; set; }

    /// <summary>
    /// Date when sales was placed
    /// </summary>
    public DateTime SalesDate { get; set; }
}
