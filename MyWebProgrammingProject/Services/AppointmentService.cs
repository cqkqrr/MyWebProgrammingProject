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

        // KURAL 1: Eğitmen o saatte çalışıyor mu? (Shift Kontrolü)
        public async Task<bool> IsTrainerAvailableAsync(int trainerId, DateTime start, DateTime end)
        {
            // Eğitmenin o tarih ve saat aralığını kapsayan bir "Availability" kaydı var mı?
            return await _context.TrainerAvailabilities.AnyAsync(a =>
                a.TrainerId == trainerId &&
                start >= a.StartTime && // Randevu başlangıcı, mesai başlangıcından sonra veya eşit olmalı
                end <= a.EndTime        // Randevu bitişi, mesai bitişinden önce veya eşit olmalı
            );
        }

        // KURAL 2: Çakışma Kontrolü (Conflict Check)
        public async Task<bool> CheckConflictAsync(int trainerId, DateTime start, DateTime end)
        {
            // Veritabanındaki diğer randevularla çakışıyor mu?
            // Formül: (YeniBaslangic < EskiBitis) VE (YeniBitis > EskiBaslangic)

            return await _context.Appointments.AnyAsync(a =>
                a.TrainerId == trainerId &&
                // a.IsApproved && // İstersen sadece onaylılar çakışsın, ama genelde bekleyenler de bloke etmeli
                start < a.EndTime &&
                end > a.StartTime
            );
        }
    }
}