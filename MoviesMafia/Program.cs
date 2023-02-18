using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesMafia.Models.Repo;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Getting Connection string
string connString = builder.Configuration.GetConnectionString("DefaultConnection");


//Getting Assembly Name
var migrationAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.

builder.Services.AddDbContext<UserContext>(options =>
options.UseSqlServer(connString, sql => sql.MigrationsAssembly(migrationAssembly)));

builder.Services.AddIdentity<ExtendedIdentityUser, IdentityRole>().AddEntityFrameworkStores<UserContext>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddServerSideBlazor();

// Add additional services, etc.
builder.Services.AddControllersWithViews();




var app = builder.Build();

var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
if (!await roleManager.RoleExistsAsync("Admin"))
{
    var adminRole = new IdentityRole("Admin");
    await roleManager.CreateAsync(adminRole);
}

// Check if the "User" role exists and create it if it doesn't
if (!await roleManager.RoleExistsAsync("User"))
{
    var userRole = new IdentityRole("User");
    await roleManager.CreateAsync(userRole);
}


var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ExtendedIdentityUser>>();
var adminUser = await userManager.FindByNameAsync("admin");
if (adminUser == null)
{
    adminUser = new ExtendedIdentityUser
    {
        UserName = "admin",
        Email = "admin@moviesmafia.com",
        EmailConfirmed = true,
        LockoutEnabled = false,
        ProfilePicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures", "admin.jpg")
    };

    var result = await userManager.CreateAsync(adminUser, "admin@Moviesmafia123");
    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LandingPage}/{action=LandingPage}/{id?}");
app.MapBlazorHub();
app.Run();



