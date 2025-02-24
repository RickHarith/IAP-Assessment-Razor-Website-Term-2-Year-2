using Coursework.Pages.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


var Configuration = builder.Configuration;

builder.Services.AddDbContext<UserDataContext>(options =>options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<UserData, IdentityRole>().AddEntityFrameworkStores<UserDataContext>();
builder.Services.AddDbContext<ProductDataContext>(options => options.UseSqlite(Configuration.GetConnectionString("Products")));
builder.Services.AddDbContext<CartDataContext>(options =>options.UseSqlite(Configuration.GetConnectionString("Cart")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	await roleManager.CreateAsync(new IdentityRole("Customer"));
	await roleManager.CreateAsync(new IdentityRole("Admin"));
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
