using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Customer : BaseModel
{
    [StringLength(50)]
    public virtual string LastName { get; set; } = string.Empty;

    [StringLength(50)]
    public virtual string FirstName { get; set; } = string.Empty;

    public virtual string Email { get; set; } = string.Empty;

    public virtual CustomerStatus CustomerStatus { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }

    public virtual ICollection<Order> Orders { get; set; }

    [Display(Name = "Vollständiger Name")]
    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }
}