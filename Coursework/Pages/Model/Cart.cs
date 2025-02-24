using System.ComponentModel.DataAnnotations;

namespace Coursework.Pages.Model
{
    public class Cart
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
