using System;

namespace maERP.Shared.Dtos.User;

public class TokenDTO
{
    public string Token { get; set; } = "";
    public DateTime Expiration { get; set; }
}