using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Dtos.Order;

public class OrderListDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;

    public DateTime DateOrdered { get; set; }
}