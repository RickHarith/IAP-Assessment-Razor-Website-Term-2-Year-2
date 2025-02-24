using Coursework.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ProductDataContext _productContext;

		public IList<Products> ProductList { get; set; }

		public IndexModel(ProductDataContext productContext)
		{
			_productContext = productContext;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			ProductList = await _productContext.Products.ToListAsync();
			return Page();
		}
	}
}