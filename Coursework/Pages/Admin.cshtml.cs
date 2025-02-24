using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Coursework.Pages.Model;
using Microsoft.AspNetCore.Authorization;


namespace Coursework.Pages
{
	[Authorize(Roles = "Admin")]
	public class AdminModel : PageModel
    {
        private readonly UserManager<UserData> _userManager;

        public AdminModel(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        public IList<UserData> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _userManager.GetUsersInRoleAsync("Customer");
        }

		public async Task<IActionResult> OnPostAsync(string id, string username, string name, string email, string password)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			// Check if email or username has been updated
			bool emailChanged = !string.IsNullOrEmpty(email) && email != user.Email;
			bool usernameChanged = !string.IsNullOrEmpty(username) && username != user.UserName;

			// Check if email or username already exists in the database
			if (emailChanged)
			{
				var existingUserWithEmail = await _userManager.FindByEmailAsync(email);
				if (existingUserWithEmail != null && existingUserWithEmail.Id != user.Id)
				{
					email = user.Email; // Restore the original email in the form
				}
			}

			if (usernameChanged)
			{
				var existingUserWithUsername = await _userManager.FindByNameAsync(username);
				if (existingUserWithUsername != null && existingUserWithUsername.Id != user.Id)
				{
					username = user.UserName; // Restore the original username in the form
				}
			}

			if (!string.IsNullOrEmpty(username))
			{
				user.UserName = username;
			}

			if (!string.IsNullOrEmpty(name))
			{
				user.Name = name;
			}

			if (!string.IsNullOrEmpty(email))
			{
				user.Email = email;
			}

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				TempData["LoginSuccessMessage"] = "User updated!";
				return RedirectToPage();
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return Page();
			}
		}

		public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["LoginSuccessMessage"] = "User deleted!";
                return RedirectToPage();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }
        }

    }
}
