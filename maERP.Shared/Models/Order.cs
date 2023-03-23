using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int Status { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Enrollment Date")]
    public DateTime OrderDate { get; set; }
}