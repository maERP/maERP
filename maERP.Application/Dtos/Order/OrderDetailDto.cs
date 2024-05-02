namespace maERP.Application.Dtos.Order;

public class OrderDetailDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;
    public int CustomerId { get; set; }
}