namespace maERP.Domain.Dtos.Settings;

public class SettingInputDto
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
