using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// DbContext
// ===============================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ===============================
// Identity + ROLLER + ŞİFRE POLİCY FIX 🔥
// ===============================
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    // 🔓 ŞİFRE KURALLARINI GEVŞETİYORUZ
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// ===============================
// MVC + Razor Pages
// ===============================
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ===============================
// 🔐 ROL + ADMIN SEED (KESİN ÇALIŞIR)
// ===============================
using (var scope = app.Services.CreateScope())
{
    await CreateRolesAndAdminAsync(scope.ServiceProvider);
}

// ===============================
// HTTP PIPELINE
// ===============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
// Program.cs içindeki app.Run(); satırından hemen öncesi
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbInitializer.SeedData(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı seed edilirken hata oluştu.");
    }
}

app.Run();
app.Run();

using (var scope = app.Services.CreateScope())
{
    await CreateRolesAndAdminAsync(scope.ServiceProvider);
    await SeedDataAsync(scope.ServiceProvider); // 👈 Bu satırı ekle
}

// =====================================================
// 🔐 ROLE + ADMIN OLUŞTURMA (FINAL, BUGSIZ)
// =====================================================
static async Task CreateRolesAndAdminAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Roller
    string[] roles = { "Admin", "Member" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 👑 ADMIN BİLGİLERİ
    string adminEmail = "b231210079@sakarya.edu.tr";
    string adminPassword = "sau";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            FullName = "Admin Kullanıcı"
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    else
    {
        // 🔁 ŞİFREYİ GARANTİYE AL
        var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
        await userManager.ResetPasswordAsync(adminUser, token, adminPassword);

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
static async Task SeedDataAsync(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

    // Eğer veritabanında hiç salon yoksa örnek bir tane ekleyelim
    if (!context.Gyms.Any())
    {
        var sampleGym = new Gym
        {
            Name = "Sau Fitness Center",
            Address = "Sakarya Üniversitesi Kampüsü",
            WorkingHours = "08:00 - 22:00"
        };
        context.Gyms.Add(sampleGym);
        await context.SaveChangesAsync();
    }
}
