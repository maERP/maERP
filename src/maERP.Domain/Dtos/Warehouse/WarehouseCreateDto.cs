using System.ComponentModel.DataAnnotations;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.Warehouse;

public class WarehouseCreateDto : IWarehouseInputModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;
}