using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Model
{
    public class CartDataContext : DbContext
    {
        public CartDataContext(DbContextOptions<CartDataContext> options) : base(options) { }

        public DbSet<Cart> ShoppingCartItems { get; set; }
    }
}