using Microsoft.AspNetCore.Identity;
using Coursework.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace Coursework.Pages
{
	public class LoginModel : PageModel
	{
		[TempData]
		public string SignUpSuccessMessage { get; set; }

        private readonly CartDataContext _cartDataContext;

        private readonly SignInManager<UserData> _signInManager;

		public LoginModel(SignInManager<UserData> signInManager, CartDataContext cartDataContext)
		{
			_signInManager = signInManager;
            _cartDataContext = cartDataContext;
        }
        public class LoginViewModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        [BindProperty]
		public LoginViewModel LoginInput { get; set; }

		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await _signInManager.SignOutAsync();
            _cartDataContext.ShoppingCartItems.RemoveRange(_cartDataContext.ShoppingCartItems);
            await _cartDataContext.SaveChangesAsync();
            TempData["LoginSuccessMessage"] = "You're now logged out, we'll miss you...";
			return RedirectToPage("/Index");
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			if (ModelState.IsValid)
			{

				var result = await _signInManager.PasswordSignInAsync(LoginInput.Username, LoginInput.Password, isPersistent: false, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					TempData["LoginSuccessMessage"] = "Login successful! Start Shopping Now!";
					return RedirectToPage("/Index");
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}

			return Page();
		}


	}
}
