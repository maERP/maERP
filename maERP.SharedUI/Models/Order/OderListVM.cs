namespace maERP.SharedUI.Models.Order;

public class OrderListVM
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
}