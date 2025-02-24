using Coursework.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages
{
    public class ProductPage9Model : PageModel
    {
        private readonly ProductDataContext _productContext;
        private readonly CartDataContext _cartContext;
        public IList<Products> ProductList { get; set; }
        public ProductPage9Model(ProductDataContext productContext, CartDataContext cartContext)
        {
            _productContext = productContext;
            _cartContext = cartContext;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _productContext.Products.ToListAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int productId)
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
