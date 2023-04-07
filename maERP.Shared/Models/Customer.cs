using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "lastname")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Column("firstname")]
    [Display(Name = "Vorname")]
    public string FirstName { get; set; } = string.Empty;

    [Column("email")]
    [Display(Name = "E-Mail")]
    public string Email { get; set; } = string.Empty;

    [Column("customer_status")]
    [Display(Name = "Kundenstatus")]
    public CustomerStatus CustomerStatus { get; set; }

    virtual public List<Address>? Addresses { get; set; }

    virtual public List<Order>? Orders { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "registriert am")]
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