using System.ComponentModel.DataAnnotations;

namespace maERP.SharedUI.Models.Order;

public class OrderVM
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
}