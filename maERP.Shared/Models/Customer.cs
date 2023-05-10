using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Customer : BaseModel
{
    [StringLength(50), Display(Name = "Vorname"), DisplayFormat(NullDisplayText = "Max")]
    public virtual string FirstName { get; set; } = string.Empty;

    [StringLength(50), Display(Name = "Nachname"), DisplayFormat(NullDisplayText = "Mustermann")]
    public virtual string LastName { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress), Display(Name = "E-Mail Adresse"), DisplayFormat(NullDisplayText = "max@mustermann.de")]
    public virtual string Email { get; set; } = string.Empty;

    [Required]
    public virtual CustomerStatus CustomerStatus { get; set; }

    public virtual ICollection<CustomerAddress>? CustomerAddresses { get; set; }

    public virtual ICollection<Order>? Orders { get; set; }

    [Display(Name = "Vollständiger Name")]
    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }
}