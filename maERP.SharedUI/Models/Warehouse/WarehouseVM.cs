using System.ComponentModel.DataAnnotations;

namespace maERP.SharedUI.Models.Warehouse;

public class WarehouseVM
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;
}