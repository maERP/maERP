using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Setting;

public class SettingDetailDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Key is required")]
    [Display(Name = "Key")]
    public string Key { get; set; } = string.Empty;

    [Required(ErrorMessage = "Value is required")]
    [Display(Name = "Value")]
    public string Value { get; set; } = string.Empty;
}
