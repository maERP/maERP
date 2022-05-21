#nullable disable

using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Data
{
	public class ApiUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}

