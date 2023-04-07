using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Order
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    public OrderStatus Status { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Enrollment Date")]
    public DateTime OrderDate { get; set; }
}