#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class SalesChannel
{
    [Required]
    [Column("id")]
    public int Id { get; set; }

    [DisplayFormat(NullDisplayText = "No type")]
    [Column("type")]
    public SalesChannelType Type { get; set; }

    [Required]
    [StringLength(200)]
    [Column("name")]
    public string Name { get; set; }

    // [Required]
    public Warehouse Warehouse { get; set; }

    [Column("url")]
    public string URL { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("import_products")]
    public bool ImportProducts { get; set; }

    [Column("import_customers")]
    public bool ImportCustomers { get; set; }

    [Column("import_orders")]
    public bool ImportOrders { get; set; }

    [Column("export_products")]
    public bool ExportProducts { get; set; }

    [Column("export_customers")]
    public bool ExportCustomers { get; set; }

    [Column("export_orders")]
    public bool ExportOrders { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("updated_at")]
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