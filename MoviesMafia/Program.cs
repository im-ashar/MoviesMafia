using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesMafia.Models.Repo;
using Microsoft.AspNetCore.Http;
using System.Net;


var builder = WebApplication.CreateBuilder(args);
//Getting Connection string
string connString = builder.Configuration.GetConnectionString("DefaultConnection");


//Getting Assembly Name
var migrationAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.

builder.Services.AddDbContext<UserContext>(options =>
options.UseSqlServer(connString, sql => sql.MigrationsAssembly(migrationAssembly)));

builder.Services.AddIdentity<ExtendedIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<UserContext>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddServerSideBlazor();

// Add additional services, etc.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
