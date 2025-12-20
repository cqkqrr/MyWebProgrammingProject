using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsTrainerAvailableAsync(int trainerId, DateTime start, DateTime end)
        {
            // Eğitmenin çalışma saatleri içinde mi?
            return await _context.TrainerAvailabilities.AnyAsync(a =>
                a.TrainerId == trainerId &&
                start >= a.StartTime &&
                end <= a.EndTime
            );
        }

        public async Task<bool> CheckConflictAsync(int trainerId, DateTime start, DateTime end)
        {
            // Çakışan başka randevu var mı?
            return await _context.Appointments.AnyAsync(a =>
                a.TrainerId == trainerId &&
                a.IsApproved &&
                (
                    (start >= a.StartTime && start < a.EndTime) ||
                    (end > a.StartTime && end <= a.EndTime) ||
                    (start <= a.StartTime && end >= a.EndTime)
                )
            );
        }
    }
}