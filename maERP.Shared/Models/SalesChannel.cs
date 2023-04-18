using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class SalesChannel : BaseModel
{
    [DisplayFormat(NullDisplayText = "No type")]
    public virtual SalesChannelType Type { get; set; }

    [StringLength(200)]
    public string Name { get; set; }

    public virtual Warehouse Warehouse { get; set; }
    
    public virtual string URL { get; set; }

    public virtual string Username { get; set; }

    public virtual string Password { get; set; }

    public virtual bool ImportProducts { get; set; }

    public virtual bool ImportCustomers { get; set; }

    public virtual bool ImportOrders { get; set; }

    public virtual bool ExportProducts { get; set; }

    public virtual bool ExportCustomers { get; set; }

    public virtual bool ExportOrders { get; set; }
}

public enum SalesChannelType
{
    notype,
    reserved00,
    reserved01,
    reserved02,
    reserved03,
    reserved04,
    reserved05,
    reserved06,
    reserved07,
    reserved08,
    reserved09,
    shopware5,
    shopware6
}