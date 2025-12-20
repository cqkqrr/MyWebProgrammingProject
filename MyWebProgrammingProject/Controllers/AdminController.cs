using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            // 1. Temel İstatistikler
            var totalMembers = await _userManager.GetUsersInRoleAsync("Member");
            var totalTrainers = await _context.Trainers.CountAsync();
            var totalAppts = await _context.Appointments.CountAsync();
            var pendingApptsCount = await _context.Appointments.CountAsync(a => !a.IsApproved);

            // 2. Bekleyen Randevular (İlk 5 tanesi)
            var pendingAppts = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Trainer)
                .Include(a => a.Service)
                .Where(a => !a.IsApproved)
                .OrderBy(a => a.StartTime)
                .Take(5)
                .ToListAsync();

            // 3. Grafik Verisi: Hangi hizmetten kaç randevu alınmış?
            var serviceStats = await _context.Appointments
                .Include(a => a.Service)
                .GroupBy(a => a.Service.Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .ToListAsync();

            var model = new AdminDashboardViewModel
            {
                TotalMembers = totalMembers.Count,
                TotalTrainers = totalTrainers,
                TotalAppointments = totalAppts,
                PendingAppointmentsCount = pendingApptsCount,
                PendingAppointments = pendingAppts,

                // Grafik verilerini ayır
                ServiceNames = serviceStats.Select(x => x.Name).ToList(),
                ServicePopularity = serviceStats.Select(x => x.Count).ToList()
            };

            return View(model);
        }
    }
}