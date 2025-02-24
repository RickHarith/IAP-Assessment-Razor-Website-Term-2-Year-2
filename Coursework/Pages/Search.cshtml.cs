using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Coursework.Pages.Model;

namespace Coursework.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ProductDataContext _productContext;
        private readonly CartDataContext _cartContext;

        public IList<Products> Products { get; set; }

        public SearchModel(ProductDataContext productContext, CartDataContext cartContext)
        {
            _productContext = productContext;
            _cartContext = cartContext;
            Products = new List<Products>();
        }

        public async Task OnGetAsync(string searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                Products = await _productContext.Products.AsNoTracking()
                    .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()))
                    .ToListAsync();
            }
        }


        public async Task<IActionResult> OnPostAddCartAsync(int productId)
        {
            // Get the product from the product context
            var product = await _productContext.Products.FindAsync(productId);

            // Get the shopping cart item from the cart context
            var cartItem = await _cartContext.ShoppingCartItems.FirstOrDefaultAsync(item => item.Name == product.Name);

            // If the cart item doesn't exist, create a new one and add it to the cart context
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };
                _cartContext.ShoppingCartItems.Add(cartItem);
            }
            // Otherwise, increment the quantity of the existing cart item
            else
            {
                cartItem.Quantity++;
                _cartContext.ShoppingCartItems.Update(cartItem);
            }
            TempData["LoginSuccessMessage"] = "Product added to shopping cart!";
            // Save changes to the cart context
            await _cartContext.SaveChangesAsync();

			// Redirect to the shopping cart page
			return Page();
		}
    }
}
