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

        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerAvailability> TrainerAvailabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Appointment -> Trainer ilişkisinde cascade delete KAPALI
            modelBuilder.Entity<Appointment>()
                .HasOne<Trainer>()               // Appointment.Trainer navigation’ı olmasa bile çalışır
                .WithMany()
                .HasForeignKey(a => a.TrainerId) // Appointment içinde int TrainerId olduğunu varsayıyorum
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
