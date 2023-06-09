using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data;
using NewsPortal.Helpers;
using NewsPortal.Helpers.Interface;
using NewsPortal.Models;
using NewsPortal.Repositories;
using NewsPortal.Repositories.Interface;
using NewsPortal.Seeder;
using NewsPortal.Seeder.Interface;
using NewsPortal.Services;
using NewsPortal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/cms/login";
    options.AccessDeniedPath = "/cms/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddNotyf(options =>
{
    options.DurationInSeconds = 10;
    options.IsDismissable = true;
    options.Position = NotyfPosition.BottomRight;
});


//Helpers
builder.Services.AddScoped<IFileHelper, FileHelper>();

builder.Services.AddScoped<IUserSeeder, UserSeeder>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();


//service
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
name: "area",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
