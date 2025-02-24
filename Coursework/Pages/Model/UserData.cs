using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Coursework.Pages.Model
{
	public class UserData : IdentityUser
	{
		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public override string Email { get; set; }
	
		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }
    }
}
