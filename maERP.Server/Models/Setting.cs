using maERP.Shared.Models;

namespace maERP.Server.Models;

public class Setting : ABaseModel
{
	public uint Section { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}