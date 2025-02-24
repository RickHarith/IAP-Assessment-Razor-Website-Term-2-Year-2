using System.Threading.Tasks;
using Coursework.Pages.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages
{
    [Authorize(Roles = "Admin")]
    public class AddProductsModel : PageModel
    {
        private readonly ProductDataContext _productContext;

        [BindProperty]
        public Products Product { get; set; }
        public IList<Products> ProductList { get; set; }

        public AddProductsModel(ProductDataContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _productContext.Products.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ProductList = await _productContext.Products.ToListAsync();
                    return Page();
                }

                await _productContext.Products.AddAsync(Product);
                await _productContext.SaveChangesAsync();
                TempData["LoginSuccessMessage"] = "Product added successfully!";
                return RedirectToPage("/AddProducts");
            }

            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqliteException;
                if (sqlException?.SqliteErrorCode == 19)
                {
					ProductList = await _productContext.Products.ToListAsync();
					ModelState.AddModelError(string.Empty, "This product already exists!");
                }
                else
                {
					ProductList = await _productContext.Products.ToListAsync();
					ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
                }

                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _productContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();
            TempData["LoginSuccessMessage"] = "Product deleted!";
            return RedirectToPage();
        }
    }
}


