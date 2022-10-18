#nullable disable

using System;

namespace maERP.Web.Models;

public class AuthenticatedUserModel
{
	public string Access_Token { get; set; }
	public string UserName { get; set; }
}