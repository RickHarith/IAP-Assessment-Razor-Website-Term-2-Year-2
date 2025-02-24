using System.Threading.Tasks;
using Coursework.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Coursework.Pages
{
    [Authorize(Roles="Customer")]
    public class ShoppingCartModel : PageModel
    {
        private readonly CartDataContext _cartDataContext;

        [BindProperty]
        public IList<Cart> Cart { get; set; }
        public decimal TotalPrice { get; private set; }

        public ShoppingCartModel(CartDataContext cartDataContext)
        {
            _cartDataContext = cartDataContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _cartDataContext.ShoppingCartItems.ToListAsync();
            TotalPrice = Cart.Sum(item => item.Price * item.Quantity);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _cartDataContext.ShoppingCartItems.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (product.Quantity > 1)
            {
                // If the product quantity is greater than 1, reduce the quantity by 1
                product.Quantity--;
                _cartDataContext.ShoppingCartItems.Update(product);
            }
            else
            {
                // If the product quantity is 1 or less, remove the product entirely from the shopping cart
                _cartDataContext.ShoppingCartItems.Remove(product);
            }

            await _cartDataContext.SaveChangesAsync();
            TempData["LoginSuccessMessage"] = "Product removed!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            _cartDataContext.ShoppingCartItems.RemoveRange(_cartDataContext.ShoppingCartItems);
            await _cartDataContext.SaveChangesAsync();
            TempData["LoginSuccessMessage"] = "All items removed from the cart!";
            return RedirectToPage();
        }
    }
}
