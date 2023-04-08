using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Customer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "lastname")]
    [Column("lastname")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "Vorname")]
    [Column("firstname")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "E-Mail")]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Kundenstatus")]
    [Column("customer_status")]
    public CustomerStatus CustomerStatus { get; set; }

    public ICollection<CustomerAddress>? CustomerAddresses { get; set; }

    public ICollection<Order>? Orders { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "registriert am")]
    [Column("enrollment_date")]
    public DateTime EnrollmentDate { get; set; }

    [Display(Name = "Vollständiger Name")]
    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }
}