using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Coursework.Pages.Model
{
	public class UserDataContext : IdentityDbContext<UserData>
	{
		public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<UserData>().HasIndex(u => u.Email).IsUnique();
		}
		public DbSet<UserData> userData { get; set; }


    }
}
