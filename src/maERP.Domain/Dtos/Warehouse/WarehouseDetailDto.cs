using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Warehouse;

public class WarehouseDetailDto
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Product Count")]
    public int ProductCount { get; set; }
}