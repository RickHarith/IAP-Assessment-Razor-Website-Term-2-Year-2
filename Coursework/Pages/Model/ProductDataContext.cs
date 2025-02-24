using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Model
{
	public class ProductDataContext : DbContext
	{
		public ProductDataContext(DbContextOptions<ProductDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Products>().HasIndex(u => u.Name).IsUnique();
        }
        public DbSet<Products> Products { get; set; }
	}
}
