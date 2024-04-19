using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class Customer : BaseEntity
{
    [StringLength(50), Display(Name = "Vorname"), DisplayFormat(NullDisplayText = "Max")]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50), Display(Name = "Nachname"), DisplayFormat(NullDisplayText = "Mustermann")]
    public string LastName { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress), Display(Name = "E-Mail Adresse"), DisplayFormat(NullDisplayText = "max@mustermann.de")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public CustomerStatus CustomerStatus { get; set; }

    public ICollection<CustomerAddress>? CustomerAddresses { get; set; }

    public ICollection<Order>? Orders { get; set; }

    [Display(Name = "Vollständiger Name")]
    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }
}