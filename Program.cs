using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesMafia.Models.Repo;
using MoviesMafia.Models.GenericRepo;
using MoviesMafia.Models;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);
//Getting Connection string
DotEnv.Load();
string PGHOST = Environment.GetEnvironmentVariable("PGHOST");
string PGPORT = Environment.GetEnvironmentVariable("PGPORT");
string PGDATABASE = Environment.GetEnvironmentVariable("PGDATABASE");
string PGUSER = Environment.GetEnvironmentVariable("PGUSER");
string PGPASSWORD = Environment.GetEnvironmentVariable("PGPASSWORD");

string connString = $"Server={PGHOST};Port={PGPORT};Database={PGDATABASE};User Id={PGUSER};Password={PGPASSWORD}";

//Getting Assembly Name
var migrationAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.

builder.Services.AddDbContext<UserContext>(options =>
options.UseNpgsql(connString, sql => sql.MigrationsAssembly(migrationAssembly)));
builder.Services.AddDbContext<RecordsDBContext>(options => options.UseNpgsql(connString, sql => sql.MigrationsAssembly(migrationAssembly)));

builder.Services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = true);
builder.Services.AddIdentity<ExtendedIdentityUser, IdentityRole>().AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IAPICalls, APICalls>();
builder.Services.AddScoped(typeof(IGenericRecordsDB<>), typeof(GenericRecordsDB<>));
builder.Services.AddServerSideBlazor();

// Add additional services, etc.
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
});


var app = builder.Build();

var scope = app.Services.CreateScope();

var migUserContext = scope.ServiceProvider.GetRequiredService<UserContext>();
migUserContext.Database.MigrateAsync().Wait();


var migRecordsContext = scope.ServiceProvider.GetRequiredService<RecordsDBContext>();
migRecordsContext.Database.MigrateAsync().Wait();

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


var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
var adminUsername = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
var adminProfilePicturePath = Environment.GetEnvironmentVariable("ADMIN_PROFILE_PICTURE_PATH");
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ExtendedIdentityUser>>();
var adminUser = await userManager.FindByNameAsync(adminUsername);
if (adminUser == null)
{
    adminUser = new ExtendedIdentityUser
    {
        UserName = adminUsername,
        Email = adminEmail,
        EmailConfirmed = true,
        LockoutEnabled = false,
        ProfilePicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures", adminProfilePicturePath)
    };
    var result = await userManager.CreateAsync(adminUser, adminPassword);
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



