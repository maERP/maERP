namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int SalesChannelId { get; set; }
    public int CustomerId { get; set; }
    public int Status { get; set; }
}