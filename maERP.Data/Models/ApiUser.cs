#nullable disable

using Microsoft.AspNetCore.Identity;

namespace maERP.Data.Models
{
	public class ApiUser : IdentityUser
	{
        public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}