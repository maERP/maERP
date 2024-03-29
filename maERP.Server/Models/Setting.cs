﻿using maERP.Server.Models;

namespace maERP.Server.Models;

public class Setting : BaseModel
{
	public int Section { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}