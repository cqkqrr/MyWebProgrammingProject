using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MemberProfile> MemberProfiles { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TrainerAvailability> TrainerAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ===============================
            // TRAINER - SERVICE MANY TO MANY
            // ===============================
            builder.Entity<Trainer>()
                .HasMany(t => t.Services)
                .WithMany(s => s.Trainers)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceTrainer",
                    j => j
                        .HasOne<Service>()
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Trainer>()
                        .WithMany()
                        .HasForeignKey("TrainersId")
                        .OnDelete(DeleteBehavior.Restrict)
                );

            // ===============================
            // APPOINTMENT → TRAINER CASCADE FIX
            // ===============================
            builder.Entity<Appointment>()
                .HasOne(a => a.Trainer)
                .WithMany()
                .HasForeignKey(a => a.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // SERVICE PRICE PRECISION
            // ===============================
            builder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);
        }
    }
}
