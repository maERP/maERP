#nullable disable

using System;
using System.ComponentModel.DataAnnotations;

namespace maERP.Web.Models;

public class AuthenticationUserModel
{
	[Required(ErrorMessage = "Email Address is required.")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Password is required")]
	public string Password { get; set; }
}