using Microsoft.AspNetCore.Identity;
using MyWebProgrammingProject.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MyWebProgrammingProject.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. Rolleri Tanımla
            string[] roles = { "Admin", "Trainer", "Member" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 2. Admin Kullanıcısını Oluştur (FullName ile)
            var adminEmail = "admin@sau.edu.tr";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin User", // Burayı güncelledik kanka
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, "Sau123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // 3. Örnek Salon Verileri
            if (!context.Gyms.Any())
            {
                context.Gyms.AddRange(
                    new Gym { Name = "Sau Fitness Center", Address = "Esentepe Kampüsü", WorkingHours = "08:00 - 22:00" },
                    new Gym { Name = "Serdivan Gym", Address = "Mavi Durak", WorkingHours = "09:00 - 23:00" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}