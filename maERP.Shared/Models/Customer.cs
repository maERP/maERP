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
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "Vorname")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "E-Mail")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Kundenstatus")]
    public CustomerStatus CustomerStatus { get; set; }

    public virtual ICollection<CustomerAddress>? CustomerAddresses { get; set; }

    public virtual ICollection<Order>? Orders { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "registriert am")]
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    [Display(Name = "Vollständiger Name")]
    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }
}