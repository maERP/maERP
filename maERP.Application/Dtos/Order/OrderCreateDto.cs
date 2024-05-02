namespace maERP.Application.Dtos.Order;

public class OrderCreateDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int SalesChannelId { get; set; }
    public int CustomerId { get; set; }    
    public int Status { get; set; }
}