#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class SalesChannel
{
    [Required]
    public int Id { get; set; }

    [DisplayFormat(NullDisplayText = "No type")]
    public SalesChannelType Type { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    // [Required]
    public virtual Warehouse Warehouse { get; set; }
    
    public string URL { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool ImportProducts { get; set; }

    public bool ImportCustomers { get; set; }

    public bool ImportOrders { get; set; }

    public bool ExportProducts { get; set; }

    public bool ExportCustomers { get; set; }

    public bool ExportOrders { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime UpdatedAt { get; set; }
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