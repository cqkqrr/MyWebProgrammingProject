using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            // 1) Roller
            string[] roles = { "Admin", "Member" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2) Admin seed bilgileri
            var adminEmail = config["AdminSeed:Email"] ?? "b231210079@sakarya.edu.tr";
            var adminPassword = config["AdminSeed:Password"] ?? "sau";
            var adminFullName = config["AdminSeed:FullName"] ?? "Arda Çakar";

            var adminNormalizedEmail = adminEmail.Trim().ToUpperInvariant();

            // Admin kullanıcıyı bul / yoksa oluştur
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = adminFullName
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createResult.Succeeded)
                    return;
            }
            else
            {
                // EmailConfirmed + FullName + UserName garanti
                var needsUpdate = false;

                if (!adminUser.EmailConfirmed)
                {
                    adminUser.EmailConfirmed = true;
                    needsUpdate = true;
                }

                if (!string.Equals(adminUser.UserName, adminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    adminUser.UserName = adminEmail;
                    needsUpdate = true;
                }

                if (!string.Equals(adminUser.FullName, adminFullName, StringComparison.Ordinal))
                {
                    adminUser.FullName = adminFullName;
                    needsUpdate = true;
                }

                if (needsUpdate)
                    await userManager.UpdateAsync(adminUser);

                // Demo için şifreyi garanti et
                var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
                await userManager.ResetPasswordAsync(adminUser, token, adminPassword);
            }

            // Admin rolünü garanti et
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                await userManager.AddToRoleAsync(adminUser, "Admin");

            // 3) ✅ Admin rolünde başka kullanıcı varsa temizle
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            foreach (var u in admins)
            {
                var uNormEmail = (u.Email ?? "").Trim().ToUpperInvariant();

                if (uNormEmail != adminNormalizedEmail)
                {
                    await userManager.RemoveFromRoleAsync(u, "Admin");

                    if (!await userManager.IsInRoleAsync(u, "Member"))
                        await userManager.AddToRoleAsync(u, "Member");
                }
            }

            // 4) Örnek Gym (boşsa)
            if (!context.Gyms.Any())
            {
                context.Gyms.Add(new Gym
                {
                    Name = "Sau Fitness Center",
                    Address = "Sakarya Üniversitesi Kampüsü",
                    WorkingHours = "08:00 - 22:00"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
