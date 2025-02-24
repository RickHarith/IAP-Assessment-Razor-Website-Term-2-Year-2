using Coursework.Pages.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages
{
	public class SignUpModel : PageModel
	{
		private readonly UserDataContext _userDataContext;
		private readonly UserManager<UserData> _userManager;

		public SignUpModel(UserDataContext userDataContext, UserManager<UserData> userManager)
		{
			_userDataContext = userDataContext;
			_userManager = userManager;
			users = new UserData();
		}

		[BindProperty]
		public UserData users { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			try
			{
				var Result = await _userManager.CreateAsync(users, users.Password);

				if (Result.Succeeded)
				{
					await _userManager.AddToRoleAsync(users, "Customer");
					TempData["SignUpSuccessMessage"] = "Sign up successful! You can now log in!";
					return RedirectToPage("/Login");
				}
				else
				{
					foreach (var error in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return Page();
				}
			}
			catch (DbUpdateException ex)
			{
				var sqlException = ex.InnerException as SqliteException;
				if (sqlException?.SqliteErrorCode == 19)
				{
					ModelState.AddModelError(string.Empty, "The email address is already in use.");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
				}

				return Page();
			}
		}
	}
}
