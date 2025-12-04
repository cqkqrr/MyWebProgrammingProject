using Microsoft.AspNetCore.Identity;          // ✔ Gerekli olan
using Microsoft.EntityFrameworkCore;           // ✔ EF Core için gerekli
using MyWebProgrammingProject.Data;            // ✔ DbContext için gerekli
using MyWebProgrammingProject.Models;          // ✔ ApplicationUser için gerekli


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ? Bu eksikti:
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
