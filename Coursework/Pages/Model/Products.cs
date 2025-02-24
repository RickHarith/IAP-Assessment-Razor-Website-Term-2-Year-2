using System.ComponentModel.DataAnnotations;

namespace Coursework.Pages.Model
{
	public class Products
	{
		[Key]
		public int ProductId { get; set; }

		[Required(ErrorMessage = "Product name is required.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Product description is required.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Product price is required.")]
		public decimal Price { get; set; }
	}
}
