using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class SalesChannel : BaseModel
{
    [DisplayFormat(NullDisplayText = "No type")]
    public virtual SalesChannelType Type { get; set; }

    [Required, StringLength(200), Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required, Display(Name = "Warenlager")]
    public virtual Warehouse Warehouse { get; set; } = new();

    [Required, StringLength(200), Display(Name = "URL")]
    public virtual string URL { get; set; } = string.Empty;

    public virtual string Username { get; set; } = string.Empty;

    public virtual string Password { get; set; } = string.Empty;

    [Required, Display(Name = "Produkte importieren")]
    public virtual bool ImportProducts { get; set; }

    [Required, Display(Name = "Kunden importieren")]
    public virtual bool ImportCustomers { get; set; }

    [Required, Display(Name = "Bestellungen importieren")]
    public virtual bool ImportOrders { get; set; }

    [Required, Display(Name = "Produkte exportieren")]
    public virtual bool ExportProducts { get; set; }

    [Required, Display(Name = "Kunden exportieren")]
    public virtual bool ExportCustomers { get; set; }

    [Required, Display(Name = "Bestellungen exportieren")]
    public virtual bool ExportOrders { get; set; }
}