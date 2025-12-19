using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Models;
using System;

namespace MyWebProgrammingProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TrainerAvailability> TrainerAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ===============================
            // 🔴 CASCADE PATH FIX (KRİTİK)
            // Appointment → Trainer ilişkisinde cascade kapatılıyor
            // ===============================
            builder.Entity<Appointment>()
                .HasOne(a => a.Trainer)
                .WithMany()
                .HasForeignKey(a => a.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // 🟡 DECIMAL PRICE FIX
            // ===============================
            builder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);

            // ===============================
            // 🔐 ROLE SEED
            // ===============================
            string adminRoleId = Guid.NewGuid().ToString();
            string memberRoleId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = memberRoleId,
                    Name = "Member",
                    NormalizedName = "MEMBER"
                }
            );

            // ===============================
            // 👤 ADMIN USER SEED
            // ===============================
            var hasher = new PasswordHasher<ApplicationUser>();
            string adminUserId = Guid.NewGuid().ToString();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = adminUserId,
                UserName = "g231210000@sakarya.edu.tr", // kendi numaran
                NormalizedUserName = "G231210000@SAKARYA.EDU.TR",
                Email = "g231210000@sakarya.edu.tr",
                NormalizedEmail = "G231210000@SAKARYA.EDU.TR",
                EmailConfirmed = true,
                FullName = "Admin Soyad",
                PasswordHash = hasher.HashPassword(null!, "sau")
            });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
            );
        }
    }
}
