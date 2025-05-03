using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();


builder.Services.AddIdentity<ApplicationUserEntity, IdentityRole>(y =>
{
    y.User.RequireUniqueEmail = true;
    y.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/auth/signinpage";
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromHours(2);
    x.Cookie.HttpOnly = true;
    x.Cookie.SameSite = SameSiteMode.None;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var app = builder.Build();


app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User" };

    foreach (var roleName in roleNames)
    {
        var exists = await roleManager.RoleExistsAsync(roleName);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUserEntity>>();
    var user = new ApplicationUserEntity { UserName = "admin@de-kaya.com", Email = "admin@de-kaya.com" };

    var userExists = await userManager.Users.AnyAsync(x => x.Email == user.Email);
    if (!userExists)
    {
        var result = await userManager.CreateAsync(user, "Dekaya2025!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }

    }
}

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
